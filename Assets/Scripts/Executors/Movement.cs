using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{


    public static void MoveForward(Movable movable)
    {
        GameObject gameObject = movable.GameObject;

        // y olmadan birim forward vektörü bulundu
        Vector3 directionVector
            = new Vector3(gameObject.transform.forward.x, 0, gameObject.transform.forward.z).normalized;

        directionVector *= movable.MovementSpeed;    // hýz vektörü oluþturuldu

        directionVector.y = movable.Rigidbody.velocity.y;   // y'nin deðiþmesini istemiyorum

        movable.Rigidbody.velocity = directionVector;       // playerýn hýzý ayarlandý

    }


    public static void Move(Movable movable, float verticalInput, float horizontalInput)
    {

        Vector3 directionVector
            = new Vector3(verticalInput, 0, horizontalInput).normalized;

        directionVector *= movable.MovementSpeed;    // hýz vektörü oluþturuldu

        directionVector.y = movable.Rigidbody.velocity.y;   // y'nin deðiþmesini istemiyorum

        movable.Rigidbody.velocity = directionVector;       // playerýn hýzý ayarlandý

    }


    public static void MovePosition(Movable movable, Vector3 targetPosition,float minDistance,float speed_)
    {
        
        GameObject gameObject = movable.GameObject;

        float distanceWithTargetPosition = Vector3.Distance(targetPosition, gameObject.transform.position);

        if (distanceWithTargetPosition > minDistance)
        {
            Vector3 directionVector = targetPosition - gameObject.transform.position;
            directionVector.y = 0;
            directionVector = directionVector.normalized;
            //float speed = Mathf.Clamp(speed_, 0, distanceWithTargetPosition);

            movable.Rigidbody.velocity = directionVector * speed_ + new Vector3(0,movable.Rigidbody.velocity.y,0);

        }
        else
        {
            movable.Rigidbody.velocity = new Vector3(0,movable.Rigidbody.velocity.y,0);
        }

    }



    public static void SpinAndMoveForward(Movable movable, float verticalInput, float horizontalInput)
    {

        Spin(movable, verticalInput, horizontalInput,15f);
        if(verticalInput !=0 || horizontalInput !=0)
            MoveForward(movable);

    }


    public static void Spin(Movable movable,float verticalInput, float horizontalInput,float gap)
    {
        Vector2 targetForward
            = new Vector2(verticalInput, horizontalInput).normalized;

        // Rotation ayarlanmasý için spin methoduna gidilir
        Spin(movable, targetForward,gap);

    }

    public static void SpinToObject(Movable movable, GameObject target,float gap)
    {
        Vector3 directionVector = VectorCalculater.CalculateDirectionVectorWithoutYAxis(
        movable.GameObject.transform.position, target.transform.position
        );

        Spin(movable, VectorCalculater.Vector3toVector2(directionVector, Axis.y).normalized,gap);

    }


    private static void Spin(Movable movable, Vector2 targetForward,float gap)
    {
        float spinSpeed = movable.SpinSpeed;

        //  dönme x ve z eksenine göre y de yapýlýyor bu yüzden 2 boyutlu bir iþlem
        //  kuþ bakýþý yapýlýyor olarak düþünülebilir

        // x ve z eksenindeki mevcut Forward vektörü bulundu
        GameObject gameObject = movable.GameObject;
        Vector2 curretForward = VectorCalculater.ThreeDForwardToTwoDForward(gameObject.transform.forward);

        //  aradaki açý bulundu
        float angleBetweenVectors = Vector2.SignedAngle(curretForward, targetForward);

        // aradaki açý küçük deðil ise iþlem yapýlýr
        if (Mathf.Abs(angleBetweenVectors) > gap)
        {
            //-- dönme yönüne göre playerýn eulerAnglesý spinSpeed*Time.deltaTime kadar deðiþtirildi
            Vector3 eulerAng = gameObject.transform.eulerAngles;
            eulerAng.y += spinSpeed * Time.deltaTime * ((angleBetweenVectors < 0) ? +1 : -1);
            gameObject.transform.eulerAngles = eulerAng;
            //---

        }

    }



}
