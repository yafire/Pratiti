using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NameBattleSystem
{
    public class enemy : MonoBehaviour
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
        public float timer;
        public float ti;
        public float force;
        public Vector3 position;
        void FixedUpdate()
        {
            if (Ani.GetCurrentAnimatorStateInfo(0).IsName("pig ability01") && BattleSystem.RCD01 <= 0)
            {
                currentFrame++;
                isAttacking = true;
            }
            else if (Ani.GetCurrentAnimatorStateInfo(0).IsName("pig ability01 hit completely") || Ani.GetCurrentAnimatorStateInfo(0).IsName("pig idle"))
            {
                isAttacking = false;
                currentFrame = 1;
            }
            if (currentFrame > 41 && Ani.GetBool("Hit completely") != true)
            {
                Ani.SetBool("Hit miss", true);
                currentFrame = 1;
                isAttacking = false;
                BattleSystem.RCD01 = BattleSystem.ability04.cd;
                StartCoroutine(CancelAbility01());
            }
            if (isInterrputed)
            {
                ti += Time.fixedDeltaTime;
                this.transform.position -= new Vector3(0.025f * distanceX, 0);
                if (isWait)
                {
                    if (distanceX > 0 && this.transform.position.x <= -distanceX + 0f + position.x)
                    {
                        this.transform.position = new Vector3(4, this.transform.position.y);
                    }
                    if (distanceX < 0 && this.transform.position.x >= -distanceX - 0f + position.x)
                    {
                        this.transform.position = new Vector3(4, this.transform.position.y);
                    }
                    if (distanceX > 0 && this.transform.position.x <= 4 && this.transform.position.y <= -0.5f)
                    {
                        rig2d.constraints = RigidbodyConstraints2D.FreezePositionY;
                        isInterrputed = false;
                        isWait = false;
                        Ani.SetBool("Hit interrupt", false);
                        this.transform.position = new Vector3(4, -1);
                    }
                    else if (distanceX < 0 && this.transform.position.x >= 4 && this.transform.position.y <= -0.5f)
                    {
                        rig2d.constraints = RigidbodyConstraints2D.FreezePositionY;
                        isInterrputed = false;
                        isWait = false;
                        Ani.SetBool("Hit interrupt", false);
                        this.transform.position = new Vector3(4, -1);
                    }
                }
            }
            else if (Ani.GetCurrentAnimatorStateInfo(0).IsName("pig interrupt") && Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                this.transform.position = new Vector3(0, 0);
                BattleSystem.enemy.hitRecoveryTime = 0;
            }
        }
        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Player" && isAttacking == true)
            {
                if (currentFrame >= 33 && currentFrame <= 41)
                {
                    print("enemycomplete");
                    GameObject.Find("battle").GetComponent<BattleSystem>().attack(BattleSystem.enemy, BattleSystem.player, BattleSystem.ability04);
                    Ani.SetBool("Hit completely", true);
                    BattleSystem.RCD01 = BattleSystem.ability04.cd;
                    currentFrame = 1;
                    isAttacking = false;
                    StartCoroutine(CancelAbility01());
                }
                else if (currentFrame < 33 && currentFrame != 1)
                {
                    print("enemyinterrupt");
                    GameObject.Find("battle").GetComponent<BattleSystem>().attack(BattleSystem.enemy, BattleSystem.player, BattleSystem.ability04); ;
                    BattleSystem.RCD01 = BattleSystem.ability04.cd;
                    currentFrame = 1;
                    isAttacking = false;
                    StartCoroutine(CancelAbility01());
                    interrupt();
                }
            }
        }
        public void interrupt()
        {
            position = Gameobj.transform.position;
            distanceX = Gameobj.transform.position.x - 4;
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
            print("敵人上升力為"+force);
            BattleSystem.enemy.hitRecoveryTimeMax = 0.5f;
            BattleSystem.enemy.hitRecoveryTime = 0.5f;
        }
        private IEnumerator CancelAbility01()
        {
            yield return new WaitForSecondsRealtime(0.03f);
            Ani.SetBool("ability01", false);
            StopCoroutine("CancelAbility01");

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


