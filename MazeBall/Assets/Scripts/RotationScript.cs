using UnityEngine;

public class RotationScript : MonoBehaviour
{
    private float distanceBetween;
    public bool canMove = false;

    void Update()
    {
        MouseDetector();

        if (distanceBetween <= MazeManager.mouseDistanceTolerance)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }

        if (Input.GetMouseButton(0) && canMove && !EndGame.gameHasEnded)
        {
            //var xPos = Input.mousePosition.x - Screen.width / 2;
            //var yPos = Input.mousePosition.y - Screen.height / 2;

            //vec3 = new Vector3(xPos, 0, yPos).normalized;

            //transform.position = transform.position + vec3;

            RotateObject();
        }

        if (EndGame.gameHasEnded)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    /// <summary>
    /// Calculates the distance between the mouse and this object. This data is used to determine if the player is near the rotationObject or not.
    /// </summary>
    private void MouseDetector()
    {
        var dist = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
        var v3Pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);

        distanceBetween = Vector3.Distance(v3Pos, transform.position);

        //print(distanceBetween);
    }

    private void RotateObject()
    {
        //Once the left mouse button is pressed, this object will rotate in order to follow the mouse. It changes the X and Y-axis, but we are only interested in the y-axis.
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.LookAt(new Vector3(mousePos.x, transform.position.y, mousePos.z));
    }
}
