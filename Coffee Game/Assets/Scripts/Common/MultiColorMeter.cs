using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiColorMeter : MonoBehaviour
{
    [SerializeField] Transform barRoot;
    [SerializeField] GameObject bg;
    private Vector2 rootSize;
    [SerializeField] GameObject barPrefab;

    private List<ColorBarData> contents = new List<ColorBarData>();
    private List<GameObject> spawnedBars = new List<GameObject>();
    private List<GameObject> ruler = new List<GameObject>();

    private void Start() {
        rootSize = (Vector2) barRoot.localScale;
    }

    // This method searches for existing color in static Colors class, if the color cannot be found by name, white is returned
    public void AddContent(string id, float val) {
        Color c = Colors.Get(id);
        AddContent(id, val, c);
    }

    public void SetContent(string id, float val, Color col) {
        if (val > 1f) return;

        int idx = contents.FindIndex(item => item.name == id);
        // -1 means it doesnt yet exist, so add it
        if (idx == -1) {
            contents.Add(new ColorBarData(id, val, col));
            UpdateBars();
            return;
        }

        var data = contents[idx];
        data = new ColorBarData(id, val, col);
        contents[idx] = data;
        UpdateBars();
    }

    private void AddContent(string id, float val, Color col) {
        if (val > 1f) return;
        if (contents.Sum(d => d.val) + val > 1f) return;

        int idx = contents.FindIndex(item => item.name == id);
        // -1 means it doesnt yet exist, so add it
        if (idx == -1) {
            contents.Add(new ColorBarData(id, val, col));
            UpdateBars();
            return;
        }

        var data = contents[idx];
        data = new ColorBarData(id, data.val + val, col);
        contents[idx] = data;
        UpdateBars();
    }

    private void UpdateBars() {
        foreach(var b in spawnedBars) {
            Destroy(b);
        }
        spawnedBars.Clear();
        float currentBottom = -rootSize.y / 2f;
        foreach(var c in contents) {
            var bar = Instantiate(barPrefab, barRoot);
            bar.GetComponentInChildren<SpriteRenderer>().color = c.color;
            bar.transform.localScale = new Vector3(rootSize.x, c.val, 1f);
            Vector3 pos = new Vector3(0f, currentBottom + c.val / 2f ,-0.01f);
            bar.transform.localPosition = pos;
            spawnedBars.Add(bar);
            currentBottom = pos.y + c.val / 2f;
        }
    }

    public void SetRuler(float bigDistance, int subMeasures) {
        foreach(var r in ruler) {
            Destroy(r);
        }
        float smallDistance = bigDistance / (subMeasures + 1);
        float currentY = -rootSize.y / 2f + smallDistance;
        int counter = 0;
        while (currentY < rootSize.y / 2f) {
            var bar = Instantiate(barPrefab, barRoot);
            bar.GetComponentInChildren<SpriteRenderer>().color = Color.black;
            float xSize = 0.25f;
            if (counter == subMeasures) {
                xSize = 0.45f;
                counter = 0;
            } else {
                counter ++;
            }
            
            bar.transform.localScale = new Vector3(xSize, 0.015f, 1f);
            Vector3 pos = new Vector3(0.5f - xSize / 2f, currentY , -0.01f);
            bar.transform.localPosition = pos;
            currentY += smallDistance;
            ruler.Add(bar);
        }
    }

    public void SetVisible(bool visible) {
        barRoot.gameObject.SetActive(visible);
        bg.SetActive(visible);
        spawnedBars.ForEach(b => b.SetActive(visible));
    }
}

public struct ColorBarData {
    public string name;
    public float val;
    public Color color;

    public ColorBarData(string name, float val, Color col) {
        this.name = name;
        this.val = val;
        this.color = col;
    }
}
