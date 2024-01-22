using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class SpaceController : NetworkBehaviour
{
    public float velocidad = 6f; // Velocidad de movimiento de la nave
    public float limiteIzquierdo = -7f; // L�mite izquierdo de la c�mara
    public float limiteDerecho = 7f; // L�mite derecho de la c�mara
    public GameObject proyectilPrefab; // Prefab del proyectil
    public Transform puntoDeDisparo; // Punto desde el cual se disparar�n los proyectiles
    public float cadenciaDeDisparo = 0.25f; // Tiempo en segundos entre disparos
    private float tiempoUltimoDisparo;

    private void Start()
    {
        if (IsOwner)
        {
            this.GetComponent<SpriteRenderer>().color = new Color (230,255,0,255);
        }
    }
    void Update()
    {
        // Obtener la entrada de teclado para el eje horizontal (izquierda/derecha)
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        // Calcular la nueva posici�n de la nave
        Vector3 nuevaPosicion = transform.position + new Vector3(movimientoHorizontal, 0, 0) * velocidad * Time.deltaTime;

        // Limitar la posici�n de la nave dentro de los l�mites de la c�mara
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzquierdo, limiteDerecho);

        // Aplicar la nueva posici�n a la nave
        transform.position = nuevaPosicion;

        if (!IsOwner)
            return;

        DisparoContinuo();

    }

    void DisparoContinuo()
    {
        if (Input.GetButton("Jump"))
        {
            if (Time.time > tiempoUltimoDisparo + cadenciaDeDisparo)
            {
                CheckFire();
                tiempoUltimoDisparo = Time.time;
            }
        }
    }
    public void CheckFire()
    {
        //if (Input.GetButtonDown("Fire1"))
            FireServerRpc(transform.rotation, puntoDeDisparo.transform.position)
            ;
    }

    [ServerRpc(RequireOwnership = false)]
    private void FireServerRpc(Quaternion rotation, Vector3 position)
    {
        GameObject bullet = Instantiate(proyectilPrefab);
        NetworkObject bulletNetwork = bullet.GetComponent<NetworkObject>();
        bulletNetwork.Spawn(true);
        bullet.transform.rotation = rotation;
        bulletNetwork.transform.rotation = rotation;
        bullet.transform.position = position;
        bulletNetwork.transform.position = position;


        Destroy(bullet, 1f);
    }

}

