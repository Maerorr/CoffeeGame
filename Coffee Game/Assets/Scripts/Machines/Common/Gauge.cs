using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gauge : MonoBehaviour
{

    public float val;

    [SerializeField] bool showTarget = true;
    [SerializeField] float minAngle, maxAngle;
    [SerializeField] float targetMinAngle, targetMaxAngle;
    public int segments;
    [SerializeField] Transform rod;
    [SerializeField] GameObject bg;
    [SerializeField] GameObject barPrefab;
    [SerializeField] MeshFilter targetMesh; 

    private List<GameObject> bars = new List<GameObject>();
    [SerializeField, Range(2, 20)] int barsCount = 10;

    private void Start() {
        targetMesh.gameObject.SetActive(showTarget);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var b in bars) {
            Destroy(b);
        }
        bars.Clear();

        float zRot = Mathf.Lerp(-minAngle, -maxAngle, val);
        rod.localEulerAngles = new Vector3(0.0f, 0.0f, zRot);

        for (int i = 0; i < barsCount; i++) {
            float angle = Mathf.Lerp(-minAngle, -maxAngle, (float)i / (barsCount - 1));
            Vector3 scale = new(0.015f, 0.05f, 1f);
            if (i == 0 || i == barsCount - 1) {
                scale = new(0.025f, 0.07f, 1f);
            }
            var bar = Instantiate(barPrefab, bg.transform);
            bar.transform.localScale = scale;
            bar.transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);
            bar.transform.localPosition = new Vector3(0.0f, 0.0f, -0.003f) + bar.transform.up * 0.45f;
            bar.GetComponentInChildren<SpriteRenderer>().color = Color.black;

            bars.Add(bar);
        }

        // var lowBound = Instantiate(barPrefab, bg.transform);
        // lowBound.transform.localScale = new Vector3(0.025f, 0.07f, 1f);
        // lowBound.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -minAngle);
        // lowBound.transform.localPosition = new Vector3(0.0f, 0.0f, -0.003f) + lowBound.transform.up * 0.5f;
        // lowBound.GetComponentInChildren<SpriteRenderer>().color = Color.black;
        // bars.Add(lowBound);

        // var highBound = Instantiate(barPrefab, bg.transform);
        // highBound.transform.localScale = new Vector3(0.025f, 0.07f, 1f);
        // highBound.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -maxAngle);
        // highBound.transform.localPosition = new Vector3(0.0f, 0.0f, -0.003f) + highBound.transform.up * 0.5f;
        // highBound.GetComponentInChildren<SpriteRenderer>().color = Color.black;
        // bars.Add(highBound);
    }

    public void ToggleTarget(bool visible) {
        showTarget = visible;
        targetMesh.gameObject.SetActive(showTarget);
    }

    private void CreateTargetZone(float minAngle, float maxAngle, int segments) {
        targetMinAngle = minAngle;
        targetMaxAngle = maxAngle;
        this.segments = segments;

        Color c = new Color(0.0f, 1f, 0.2f, 0.7f);
            Mesh mesh = new();
            Vector3 def = new(0.0f, 0.45f, 0.01f);
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<Color> cols = new List<Color>();

            Vector3 lowBot = Quaternion.AngleAxis(-targetMinAngle, Vector3.forward) * (def * 0.9f);
            Vector3 lowTop = Quaternion.AngleAxis(-targetMinAngle, Vector3.forward) * def;

            vertices.Add(lowBot);
            vertices.Add(lowTop);

            cols.Add(c);
            cols.Add(c);

            for (int i = 0; i <= segments; i++) {
                float currentAngle = Mathf.Lerp(-targetMinAngle, -targetMaxAngle, (float)(i + 1) / (segments + 1));
                Vector3 v1 = Quaternion.AngleAxis(currentAngle, Vector3.forward) * (def * 0.9f);
                Vector3 v2 = Quaternion.AngleAxis(currentAngle, Vector3.forward) * def;
                vertices.Add(v1);
                vertices.Add(v2);

                cols.Add(c);
                cols.Add(c);

                triangles.Add(i * 2);
                triangles.Add(i * 2 + 1);
                triangles.Add(i * 2 + 3);

                triangles.Add(i * 2);
                triangles.Add(i * 2 + 3);
                triangles.Add(i * 2 + 2);
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.colors = cols.ToArray();

            targetMesh.mesh = mesh;
    }
}
