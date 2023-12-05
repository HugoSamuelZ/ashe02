using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Bombado : MonoBehaviour
{
    public int hp = 5;
    public float velocidade = 0.1f;
    public float distInicial = -0.5f;
    public float distFinal = 2f;
    private SpriteRenderer ImagemSlime;
    public Animator anim;
    private Transform posicaoDoJogador;
    public float velocidadeDoInimigo;

    void Start()
    {
        posicaoDoJogador = GameObject.FindGameObjectWithTag("Jogador_Principal").transform;
        ImagemSlime = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        SeguirJogador();
        Andar();
    }


    void Andar()
    {
        transform.position = new Vector3(transform.position.x + velocidade, transform.position.y, transform.position.z);
        //mudar velocidade
        //Andar para trás
        if (transform.position.x > distFinal)
        {
            velocidade = velocidade * -1;
            ImagemSlime.flipX = true;
        }
        //Andar para frente
        if (transform.position.x < distInicial)
        {
            velocidade = velocidade * -1f;
            ImagemSlime.flipX = false;
        }
    }

    private void SeguirJogador()
    {
        if (posicaoDoJogador.gameObject != null)
        {
            float distance = Vector2.Distance(transform.position, posicaoDoJogador.position);
            if (distance > 1 && distance < 4)
            {
                transform.position = Vector2.MoveTowards(transform.position, posicaoDoJogador.position, velocidadeDoInimigo * Time.deltaTime);
            }
            else if (distance <= 1) 
            {
                anim.SetTrigger("atk");
            }
        }

    }

    ///Danos

    // verifica dano recebido dos inimigos
    void OnTriggerEnter2D(Collider2D colisao)
    {

        if (colisao.gameObject.tag == "Fogo")
        {


            hp--;
            if (hp <= 0)
            {
             

                Destroy(this.gameObject);
               

            }

        }
        if (colisao.gameObject.tag == "Jogador_Principal")


            hp--;
        if (hp <= 0)

        {
            Destroy(this.gameObject);
        }
    }
}

