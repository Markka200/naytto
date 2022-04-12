using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVision : MonoBehaviour
{
    private Mesh mesh;
    public Material material;
    public GameObject enemy;
    private Vector3 origin;
    enemy Enemy;
    private float fov;
    private float haluttufov;
    private float StartingAngle;
   
   
    // Start is called before the first frame update
    void Start()
    {
         mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        Enemy = enemy.GetComponent<enemy>();
        origin = enemy.transform.position;
        fov = 360f;
        haluttufov = 45f;
        
      
    }
    private void LateUpdate()
    {
        origin = enemy.transform.position;
        setaimdirection(enemy.transform.up );



        int raycount = 250;
        float angle = StartingAngle + 90;
        float angleincrease = fov / raycount;
        float viewdistance = 20f;
        float smallviewdistance = 2f;
        float jakaja = fov / haluttufov;  // 360 / 8 = 45 eli 45 astetta on se iso homma
        


        Vector3[] vertices = new Vector3[raycount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[raycount * 3];

        vertices[0] = origin; // origin
        int vertexindex = 1;
        int triangelindex = 0;
        for(int i = 0; i <= raycount; i++)
        {

                LayerMask mask = LayerMask.GetMask("Wall", "player");
                Vector3 vertex;       
            if (i <= (raycount / jakaja))
            {
                RaycastHit2D raycastH = Physics2D.Raycast(origin, new Vector3(Mathf.Cos(angle * (Mathf.PI / 180f)), Mathf.Sin(angle * (Mathf.PI / 180f))), viewdistance, mask);

                if (raycastH.collider == null)
                {

                    vertex = origin + new Vector3(Mathf.Cos(angle * (Mathf.PI / 180f)), Mathf.Sin(angle * (Mathf.PI / 180f))) * viewdistance;

                }
                else
                {

                    vertex = origin + new Vector3(Mathf.Cos(angle * (Mathf.PI / 180f)), Mathf.Sin(angle * (Mathf.PI / 180f))) * raycastH.distance;

                }
                if (raycastH.collider?.gameObject.layer == LayerMask.NameToLayer("player") && Enemy.GetComponent<enemy>().VisionHu == true) // että pelaaja huomataan
                {
                    Enemy.Invoke("VisionFound", 0);
                }
                else if (Enemy.GetComponent<enemy>().VisionHu == false)
                {
                    Enemy.Invoke("VisionLost", 0);
                }
            }
            else
            {
                RaycastHit2D raycastH = Physics2D.Raycast(origin, new Vector3(Mathf.Cos(angle * (Mathf.PI / 180f)), Mathf.Sin(angle * (Mathf.PI / 180f))), smallviewdistance, mask);

                if (raycastH.collider == null)
                {

                    vertex = origin + new Vector3(Mathf.Cos(angle * (Mathf.PI / 180f)), Mathf.Sin(angle * (Mathf.PI / 180f))) * smallviewdistance;

                }
                else
                {

                    vertex = origin + new Vector3(Mathf.Cos(angle * (Mathf.PI / 180f)), Mathf.Sin(angle * (Mathf.PI / 180f))) * raycastH.distance;

                }
                if (raycastH.collider?.gameObject.layer == LayerMask.NameToLayer("player") && Enemy.GetComponent<enemy>().VisionHu == true) // että pelaaja huomataan
                {
                    Enemy.Invoke("VisionFound", 0);
                }
                else if (Enemy.GetComponent<enemy>().VisionHu == false)
                {
                    Enemy.Invoke("VisionLost", 0);
                }
            }
                vertices[vertexindex] = vertex;

                if (i > 0)
                {
                    triangles[triangelindex + 0] = 0;
                    triangles[triangelindex + 1] = vertexindex - 1;
                    triangles[triangelindex + 2] = vertexindex;

                    triangelindex += 3;
                }




                vertexindex++;
                angle -= angleincrease;
            
        }


        

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;



        // GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        // gameObject.transform.localScale = new Vector3();

        // gameObject.GetComponent<MeshRenderer>().material= material;
        // gameObject.GetComponent<MeshFilter>().mesh = mesh;
        //  gameObject.transform.localScale = new Vector3(1, 1, 1);

    }

    public void setaimdirection(Vector3 aimDirection)
    {
        StartingAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        if (StartingAngle < 0) {StartingAngle += 360; }
        StartingAngle = (StartingAngle - (fov / 2f )) - 270 + 22.5f;    
    }
  



   
}
