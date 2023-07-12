using UnityEngine;
using Transform = ULTRANET.Core.Protobuf.Transform;

namespace ULTRANET.Core
{
    public class TransformUtils
    {
        public static (Vector3 pos, Vector3 rot, Vector3 scale) FromTransform(Transform transform)
        {
            Vector3 pos = new Vector3((float)transform.PosX, (float)transform.PosY, (float)transform.PosZ);
            Vector3 rot = new Vector3((float)transform.RotX, (float)transform.RotY, (float)transform.RotZ);
            Vector3 scale = new Vector3((float)transform.SclX, (float)transform.SclY, (float)transform.SclZ);

            return (pos, rot, scale);
        }

        public static Transform ToTransform(Vector3 pos, Vector3 rot, Vector3 scale)
        {
            // Round to 2 decimal places
            pos.x = Mathf.Round(pos.x * 100f) / 100f;
            pos.y = Mathf.Round(pos.y * 100f) / 100f;
            pos.z = Mathf.Round(pos.z * 100f) / 100f;

            rot.x = Mathf.Round(rot.x * 100f) / 100f;
            rot.y = Mathf.Round(rot.y * 100f) / 100f;
            rot.z = Mathf.Round(rot.z * 100f) / 100f;

            scale.x = Mathf.Round(scale.x * 100f) / 100f;
            scale.y = Mathf.Round(scale.y * 100f) / 100f;
            scale.z = Mathf.Round(scale.z * 100f) / 100f;

            return new Transform()
            {
                PosX = pos.x, PosY = pos.y, PosZ = pos.z,
                RotX = rot.x, RotY = rot.y, RotZ = rot.z,
                SclX = scale.x, SclY = scale.y, SclZ = scale.z
            };
        }
    }
}