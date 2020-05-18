using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NameBattleSystem
{
    public class Player : MonoBehaviour
    {
        public GameObject Gameobj;
        public Animator Ani;
        public Rigidbody2D rig2d;
        public int hitStatus = 0;
        public int currentFrame = 1;
        public bool isAttacking = false;
        public bool isInterrputed = false;
        public bool isWait = false;
        public float distanceX;
        public float ti;
        public float force;
        public Vector3 position;
        void Start()
        {
            /*1.65f
             2.3f
             3.5f
             */
             
        }
        void FixedUpdate()
        {
            if (Ani.GetCurrentAnimatorStateInfo(0).IsName("player ability03") && BattleSystem.LCD <= 0)
            {
                currentFrame++;
                isAttacking = true;
                Ani.SetInteger("Status", -1);
            }
            else if (Ani.GetCurrentAnimatorStateInfo(0).IsName("feather ability01 hit completely") || Ani.GetCurrentAnimatorStateInfo(0).IsName("play idle"))
            {
                isAttacking = false;
                currentFrame = 1;
            }
            if (currentFrame > 160)
            {
                Ani.SetBool("Hit miss", true);
                currentFrame = 1;
                isAttacking = false;
                BattleSystem.LCD = BattleSystem.ability01.cd;
            }
            if (isInterrputed)
            {
                ti += Time.fixedDeltaTime;
                this.transform.position -= new Vector3(0.025f * distanceX, 0);
                if (isWait)
                {
                    if (distanceX > 0 && this.transform.position.x <= -distanceX + 0f + position.x)
                    {
                        this.transform.position = new Vector3(-4, this.transform.position.y);
                    }
                    if (distanceX < 0 && this.transform.position.x >= -distanceX - 0f + position.x)
                    {
                        this.transform.position = new Vector3(-4, this.transform.position.y);
                    }
                    if (distanceX > 0 && this.transform.position.x <= -4 && this.transform.position.y <= -0.263f)
                    {
                        rig2d.constraints = RigidbodyConstraints2D.FreezePositionY;
                        isInterrputed = false;
                        isWait = false;
                        Ani.SetBool("Hit interrupt", false);
                        Ani.SetInteger("Status", 0);
                        this.transform.position = new Vector3(-4, -0.517f);
                    }
                    else if (distanceX < 0 && this.transform.position.x >= -4 && this.transform.position.y <= -0.263f)
                    {
                        rig2d.constraints = RigidbodyConstraints2D.FreezePositionY;
                        isInterrputed = false;
                        isWait = false;
                        Ani.SetBool("Hit interrupt", false);
                        Ani.SetInteger("Status", 0);
                        this.transform.position = new Vector3(-4, -0.517f);
                    }
                }
            }
            else if (Ani.GetCurrentAnimatorStateInfo(0).IsName("feather interrupt") && Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) 
            {
                this.transform.position = new Vector3(0, 0);
                BattleSystem.player.hitRecoveryTime = 0;
            }
        }
        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Enemy" && isAttacking == true)
            {
                if (currentFrame >= 156 && currentFrame <= 160)
                {
                    print("playercomplete");
                    GameObject.Find("battle").GetComponent<BattleSystem>().attack(BattleSystem.player, BattleSystem.enemy, BattleSystem.ability01);
                    Ani.SetBool("Hit completely", true);
                    BattleSystem.LCD = BattleSystem.ability01.cd;
                    currentFrame = 1;
                    isAttacking = false;
                }
                else if (currentFrame < 156 && currentFrame != 1)
                {
                    print("playerinterrupt");
                    GameObject.Find("battle").GetComponent<BattleSystem>().attack(BattleSystem.player, BattleSystem.enemy, BattleSystem.ability01);
                    BattleSystem.LCD = BattleSystem.ability01.cd;
                    currentFrame = 1;
                    isAttacking = false;
                    interrupt();
                }
            }
        }
        public void interrupt()
        {
            position = Gameobj.transform.position;
            distanceX = Gameobj.transform.position.x + 4;
            force = 85 / distanceX;
            if (force < 0) 
            {
                force = -force;
            }
            if (force > 30) 
            {
                force = 30;
            }
            else if (force < 25)
            {
                force = 25;
            }
            isInterrputed = true;
            rig2d.constraints = RigidbodyConstraints2D.None;
            rig2d.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            Ani.SetBool("Hit interrupt", true);
            StartCoroutine(Position());
            print("玩家上升力為" + force);
            BattleSystem.player.hitRecoveryTimeMax = 0.95f;
            BattleSystem.player.hitRecoveryTime = 0.95f;
        }
        IEnumerator Wait()
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            isWait = true;
            StopCoroutine("Wait");
        }
        IEnumerator Position()
        {
            StartCoroutine(Wait());
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            this.transform.position = position;
            StopCoroutine("Position");
        }
    }
}