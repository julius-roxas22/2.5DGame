using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum TransitionParameters
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
    }

    public class CharacterControl : MonoBehaviour
    {
        public float movementSpeed;
        public Animator skinnedMeshAnimator;
        public Material mat;
        public GameObject SphereEdgePrefab;
        public List<GameObject> BottomSpheres = new List<GameObject>();
        public List<GameObject> FrontSpheres = new List<GameObject>();
        public float GravityMultiplier;
        public float PullMultiplier;

        public List<Collider> RagdollParts = new List<Collider>();
        public List<Collider> CollidingParts = new List<Collider>();

        public bool Jump;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Attack;

        private Rigidbody rigid;

        public Rigidbody RIGID_BODY
        {
            get
            {
                if (null == rigid)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }

        private void Awake()
        {
            bool SwitchBack = false;

            if (!IsFacingForward())
            {
                SwitchBack = true;
            }

            SetFaceForward(true);

            SetUpRagdollParts();
            SetUpSphereEdge();

            if (SwitchBack)
            {
                SetFaceForward(false);
            }
        }

        private void SetUpRagdollParts()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();

            foreach (Collider col in colliders)
            {
                if (col.gameObject != gameObject)
                {
                    col.isTrigger = true;
                    RagdollParts.Add(col);
                }
            }
        }

        //private IEnumerator Start()
        //{
        //    yield return new WaitForSeconds(5f);
        //    RIGID_BODY.AddForce(Vector3.up * 200f);
        //    yield return new WaitForSeconds(0.5f);
        //    TurnOnRagdoll();
        //}

        public void TurnOnRagdoll()
        {
            RIGID_BODY.velocity = Vector3.zero;
            RIGID_BODY.useGravity = false;
            GetComponent<BoxCollider>().enabled = false;
            skinnedMeshAnimator.avatar = null;

            foreach (Collider col in RagdollParts)
            {
                col.attachedRigidbody.velocity = Vector3.zero;
                col.isTrigger = false;
            }
        }

        private void FixedUpdate()
        {
            if (RIGID_BODY.velocity.y < 0f)
            {
                RIGID_BODY.velocity -= Vector3.up * GravityMultiplier;
            }

            if (RIGID_BODY.velocity.y > 0f && !Jump)
            {
                RIGID_BODY.velocity -= Vector3.up * PullMultiplier;
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (RagdollParts.Contains(col))
            {
                return;
            }

            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

            if (null == control)
            {
                return;
            }

            if (col.gameObject == control.gameObject)
            {
                return;
            }

            if (!CollidingParts.Contains(col))
            {
                CollidingParts.Add(col);
            }

        }

        private void OnTriggerExit(Collider col)
        {
            if (CollidingParts.Contains(col))
            {
                CollidingParts.Remove(col);
            }
        }

        private void SetUpSphereEdge()
        {
            BoxCollider box = GetComponent<BoxCollider>();

            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float top = box.bounds.center.y + box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject bottomBack = CreatePrefabSphereEdge(new Vector3(0f, bottom, back));
            GameObject bottomFront = CreatePrefabSphereEdge(new Vector3(0f, bottom, front));
            GameObject topFront = CreatePrefabSphereEdge(new Vector3(0f, top, front));

            BottomSpheres.Add(bottomBack);
            BottomSpheres.Add(bottomFront);

            FrontSpheres.Add(topFront);
            FrontSpheres.Add(bottomFront);

            float horSphereSection = (bottomBack.transform.position - bottomFront.transform.position).magnitude / 5f;
            CreatePrefabSphereEdge(bottomBack.transform, bottomBack.transform.forward, horSphereSection, 4, BottomSpheres);

            float verSphereSection = (bottomFront.transform.position - topFront.transform.position).magnitude / 10f;
            CreatePrefabSphereEdge(bottomFront.transform, bottomFront.transform.up, verSphereSection, 9, FrontSpheres);
        }

        private void CreatePrefabSphereEdge(Transform start, Vector3 direction, float section, int iteration, List<GameObject> spheres)
        {
            for (int i = 0; i < iteration; i++)
            {
                Vector3 position = start.transform.position + (direction * section * (i + 1));
                GameObject obj = CreatePrefabSphereEdge(position);
                spheres.Add(obj);
            }
        }

        private GameObject CreatePrefabSphereEdge(Vector3 position)
        {
            return Instantiate(SphereEdgePrefab, position, Quaternion.identity, transform);
        }

        public void MoveAbleCharacter(float Speed, float SpeedGraph)
        {
            transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);
        }

        public void changeMaterial()
        {
            if (null == mat)
            {
                Debug.LogError("No Material Specified");
            }

            Renderer[] arr = GetComponentsInChildren<Renderer>();

            foreach (Renderer r in arr)
            {
                if (this.gameObject != r.gameObject)
                {
                    r.material = mat;
                }
            }
        }

        public void SetFaceForward(bool isFacing)
        {
            if (isFacing)
            {
                transform.rotation = Quaternion.identity;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0);
            }
        }

        public bool IsFacingForward()
        {
            if (transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

