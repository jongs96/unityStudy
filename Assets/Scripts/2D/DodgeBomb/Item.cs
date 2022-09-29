using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DodgeBomb;

namespace DodgeBomb
{ 
    public class Item : MonoBehaviour
    {
        public Sprite[] Imagelist;
        public GameObject[] Effectlist;
        public SpriteRenderer myRenderer;
        public enum STATE
        {
            Create, Active
        }
        public enum TYPE
        {
            Bomb, Coin
        }
        public STATE myState = STATE.Create;
        public TYPE myType = TYPE.Bomb;
        public float MoveSpeed = 2.0f;

        Dropper myDropper = null;
        void ChangeState(STATE s)
        {
            if (myState == s) return;
            myState = s;
            switch(myState)
            {
                case STATE.Create:
                    break;
                case STATE.Active:
                    break;
            }
        }

        void StateProcess()
        {
            switch (myState)
            {
                case STATE.Create:
                    break;
                case STATE.Active:
                    transform.Translate(Vector3.down * Time.deltaTime * MoveSpeed, Space.World);
                    break;
            }
        }

        public void Inialize(TYPE type, Dropper drop)
        {
            myDropper = drop;
            myType = type;
            switch(myType)
            {
                case TYPE.Bomb:
                    break;
                case TYPE.Coin:
                    break;
            }
            myRenderer.sprite = Imagelist[(int)myType];

            ChangeState(STATE.Active);
        }

        public void ItemDestroy()
        {
            myDropper.ItemDestroy -= ItemDestroy;

            Instantiate(Effectlist[(int)myType],transform.position,Quaternion.identity);

            Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {            
        }

        // Update is called once per frame
        void Update()
        {
            StateProcess();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {               
                ItemDestroy();

                switch (myType)
                {
                    case TYPE.Bomb:
                        PlayMain.Inst.UpdateLife(-1);
                        break;
                    case TYPE.Coin:
                        PlayMain.Inst.UpdateScore(10);
                        GameObject obj = Instantiate(Resources.Load("Prefabs/UI/ScoreNumber"),PlayMain.Inst.myScoreUITransform) as GameObject;
                        obj.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                        obj.GetComponent<ScoreNumber>().Initialize(10);
                        break;
                }
            }
            else if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {                
                ItemDestroy();
            }
        }
    }
}
