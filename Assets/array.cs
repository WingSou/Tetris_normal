using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class array : MonoBehaviour
{

    [SerializeField]
    private GameObject cube;

    [SerializeField]
    public GameObject GameOverTextObject;

    [SerializeField]
    public GameObject ButtonTextObject;

    [SerializeField]
    public GameObject ContinueTextObjct;

    [SerializeField]
    public GameObject MoveTextObject;

    [SerializeField]
    public GameObject L_rotateTextObjct;

    [SerializeField]
    public GameObject R_rotateTextObjct;

    [SerializeField]
    public GameObject DeleteTextObject;

    [SerializeField]
    public GameObject BGMVolumeObject;

    public static GameObject[,] cubes = new GameObject[14, 23];

    //コア
    int x;
    int y;

    //生成座標
    private int reference_x = 6;
    private int reference_y = 21;

    //再生成制御用
    bool control_move;

    //テキスト削除用
    bool text_delete = true;

    //ブロック削除時の再生成制御
    bool control_delite;

    //ブロックの削除マスの記憶
    List<int> delite_number = new List<int>();

    //ブロックの削除命令確認
    bool delite_check;
    int check_count = 0;

    //削除中か判断
    bool deleting;

    //ボタンが押されているか判断
    bool left;
    bool right;
    bool rotate_L;
    bool rotate_R;

    //再生成中のボタン入力無効化用
    bool unable;

    //どのぐらい経過で固定化するか
    float times = 0.3f;

    //限界の場所で何秒動かせるか
    float rimit = 1.0f;

    //ブロックの落下スピード
    float block_speed;

    //基本の落下スピード
    float defult_block_speed = 1.0f;

    //操作加速用
    float accele_speed = 0.0f;

    //レベルアップ加速用(マイナスで加速)
    float level_speed = 0.0f;

    //モード切替用
    int mode;

    void BlockType(int i, int r, bool rewrite)
    {
        if (cubes[i, r].GetComponent<color_v2>().control_cube == true)
        {
            switch (cubes[i, r].GetComponent<color_v2>().cube_type)
            {
                //I
                case 1:

                    switch (cubes[i, r].GetComponent<color_v2>().pos_type)
                    {
                        //0°
                        case 0:

                            cubes[i - 2, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 2, r].GetComponent<color_v2>().color_number = 1;
                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 1;
                            cubes[i, r].GetComponent<color_v2>().color_number = 1;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 1;

                            break;

                        //90°
                        case 1:

                            cubes[i, r + 2].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 2].GetComponent<color_v2>().color_number = 1;
                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 1;
                            cubes[i, r].GetComponent<color_v2>().color_number = 1;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 1;

                            break;

                        //-90°
                        case 2:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 2].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 1;
                            cubes[i, r].GetComponent<color_v2>().color_number = 1;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 1;
                            cubes[i, r - 2].GetComponent<color_v2>().color_number = 1;

                            break;

                        //180°
                        case 3:

                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 2, r].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 1;
                            cubes[i, r].GetComponent<color_v2>().color_number = 1;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 1;
                            cubes[i + 2, r].GetComponent<color_v2>().color_number = 1;

                            break;
                    }

                    break;

                //O
                case 2:

                    switch (cubes[i, r].GetComponent<color_v2>().pos_type)
                    {
                        //0°
                        case 0:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 2;
                            cubes[i + 1, r + 1].GetComponent<color_v2>().color_number = 2;
                            cubes[i, r].GetComponent<color_v2>().color_number = 2;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 2;

                            break;

                        //90°
                        case 1:

                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r].GetComponent<color_v2>().color_number = 2;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 2;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 2;
                            cubes[i + 1, r - 1].GetComponent<color_v2>().color_number = 2;

                            break;

                        //-90°
                        case 2:

                            cubes[i - 1, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r + 1].GetComponent<color_v2>().color_number = 2;
                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 2;
                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 2;
                            cubes[i, r].GetComponent<color_v2>().color_number = 2;

                            break;

                        //180°
                        case 3:

                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r - 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 2;
                            cubes[i, r].GetComponent<color_v2>().color_number = 2;
                            cubes[i - 1, r - 1].GetComponent<color_v2>().color_number = 2;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 2;

                            break;
                    }

                    break;

                //S
                case 3:

                    switch (cubes[i, r].GetComponent<color_v2>().pos_type)
                    {
                        //0°
                        case 0:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 3;
                            cubes[i + 1, r + 1].GetComponent<color_v2>().color_number = 3;
                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 3;
                            cubes[i, r].GetComponent<color_v2>().color_number = 3;

                            break;

                        //90°
                        case 1:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 3;
                            cubes[i, r].GetComponent<color_v2>().color_number = 3;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 3;
                            cubes[i + 1, r - 1].GetComponent<color_v2>().color_number = 3;

                            break;

                        //-90°
                        case 2:

                            cubes[i - 1, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r + 1].GetComponent<color_v2>().color_number = 3;
                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 3;
                            cubes[i, r].GetComponent<color_v2>().color_number = 3;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 3;

                            break;

                        //180°
                        case 3:

                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 3;
                            cubes[i, r].GetComponent<color_v2>().color_number = 3;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 3;
                            cubes[i - 1, r - 1].GetComponent<color_v2>().color_number = 3;

                            break;
                    }

                    break;

                //Z
                case 4:

                    switch (cubes[i, r].GetComponent<color_v2>().pos_type)
                    {
                        //0°
                        case 0:

                            cubes[i - 1, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r + 1].GetComponent<color_v2>().color_number = 4;
                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 4;
                            cubes[i, r].GetComponent<color_v2>().color_number = 4;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 4;

                            break;

                        //90°
                        case 1:

                            cubes[i + 1, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i + 1, r + 1].GetComponent<color_v2>().color_number = 4;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 4;
                            cubes[i, r].GetComponent<color_v2>().color_number = 4;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 4;

                            break;

                        //-90°
                        case 2:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 4;
                            cubes[i, r].GetComponent<color_v2>().color_number = 4;
                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 4;
                            cubes[i - 1, r - 1].GetComponent<color_v2>().color_number = 4;

                            break;

                        //180°
                        case 3:

                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 4;
                            cubes[i, r].GetComponent<color_v2>().color_number = 4;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 4;
                            cubes[i + 1, r - 1].GetComponent<color_v2>().color_number = 4;

                            break;
                    }

                    break;

                //J
                case 5:

                    switch (cubes[i, r].GetComponent<color_v2>().pos_type)
                    {
                        //0°
                        case 0:

                            cubes[i - 1, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r + 1].GetComponent<color_v2>().color_number = 5;
                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 5;
                            cubes[i, r].GetComponent<color_v2>().color_number = 5;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 5;

                            break;

                        //90°
                        case 1:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 5;
                            cubes[i + 1, r + 1].GetComponent<color_v2>().color_number = 5;
                            cubes[i, r].GetComponent<color_v2>().color_number = 5;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 5;

                            break;

                        //-90°
                        case 2:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r - 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 5;
                            cubes[i, r].GetComponent<color_v2>().color_number = 5;
                            cubes[i - 1, r - 1].GetComponent<color_v2>().color_number = 5;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 5;

                            break;

                        //180°
                        case 3:

                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 5;
                            cubes[i, r].GetComponent<color_v2>().color_number = 5;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 5;
                            cubes[i + 1, r - 1].GetComponent<color_v2>().color_number = 5;

                            break;
                    }

                    break;

                //L
                case 6:

                    switch (cubes[i, r].GetComponent<color_v2>().pos_type)
                    {
                        //0°
                        case 0:

                            cubes[i + 1, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i + 1, r + 1].GetComponent<color_v2>().color_number = 6;
                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 6;
                            cubes[i, r].GetComponent<color_v2>().color_number = 6;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 6;

                            break;

                        //90°
                        case 1:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 6;
                            cubes[i, r].GetComponent<color_v2>().color_number = 6;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 6;
                            cubes[i + 1, r - 1].GetComponent<color_v2>().color_number = 6;

                            break;

                        //-90°
                        case 2:

                            cubes[i - 1, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r + 1].GetComponent<color_v2>().color_number = 6;
                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 6;
                            cubes[i, r].GetComponent<color_v2>().color_number = 6;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 6;

                            break;

                        //180°
                        case 3:

                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 6;
                            cubes[i, r].GetComponent<color_v2>().color_number = 6;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 6;
                            cubes[i - 1, r - 1].GetComponent<color_v2>().color_number = 6;

                            break;
                    }

                    break;

                //T
                case 7:

                    switch (cubes[i, r].GetComponent<color_v2>().pos_type)
                    {
                        //0°
                        case 0:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 7;
                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 7;
                            cubes[i, r].GetComponent<color_v2>().color_number = 7;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 7;

                            break;

                        //90°
                        case 1:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 7;
                            cubes[i, r].GetComponent<color_v2>().color_number = 7;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 7;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 7;

                            break;

                        //-90°
                        case 2:

                            cubes[i, r + 1].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i, r + 1].GetComponent<color_v2>().color_number = 7;
                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 7;
                            cubes[i, r].GetComponent<color_v2>().color_number = 7;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 7;

                            break;

                        //180°
                        case 3:

                            cubes[i - 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i + 1, r].GetComponent<color_v2>().c_swich = rewrite;
                            cubes[i, r - 1].GetComponent<color_v2>().c_swich = rewrite;

                            cubes[i - 1, r].GetComponent<color_v2>().color_number = 7;
                            cubes[i, r].GetComponent<color_v2>().color_number = 7;
                            cubes[i + 1, r].GetComponent<color_v2>().color_number = 7;
                            cubes[i, r - 1].GetComponent<color_v2>().color_number = 7;

                            break;
                    }

                    break;
            }
        }
    }

    //遅延用
    private IEnumerator Delay(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    //再生成用
    void Rebuild()
    {
        if (cubes[x, y].GetComponent<color_v2>().can_make == true)
        {
        //    StartCoroutine(Delay(times, () =>
        //{
            cubes[x, y].GetComponent<color_v2>().control_cube = false;

            x = reference_x;
            y = reference_y;
            accele_speed = 0.0f;

            cubes[x, y].GetComponent<color_v2>().cube_type = UnityEngine.Random.Range(1, 7);
            cubes[x, y].GetComponent<color_v2>().pos_type = 0;
            cubes[x, y].GetComponent<color_v2>().control_cube = true;

            BlockType(x, y, true);

            
        //}));

            cubes[x, y].GetComponent<color_v2>().can_make = false;
            StartCoroutine(Delay(1.0f, () =>
            {

                control_move = true;

            }));
            
            unable = false;
            defult_block_speed = 1.0f;
        }
        
    }

    //壁、床の判断4マス用（確認する座標×４、再生成させるか、 移動か回転か、どこに移動するか（詳しくはFloorDecision））
    void Decision_calculate4(int floor_count_x1, int floor_count_y1, int floor_count_x2, int floor_count_y2,
                            int floor_count_x3, int floor_count_y3, int floor_count_x4, int floor_count_y4,
                            bool create, bool move_or_rotate, int move_x, int move_y)
    {
        bool delite = create;
        bool answer = move_or_rotate;

        if (cubes[floor_count_x1, floor_count_y1].GetComponent<color_v2>().c_swich == false &&
                           cubes[floor_count_x2, floor_count_y2].GetComponent<color_v2>().c_swich == false &&
                           cubes[floor_count_x3, floor_count_y3].GetComponent<color_v2>().c_swich == false &&
                           cubes[floor_count_x4, floor_count_y4].GetComponent<color_v2>().c_swich == false)
        {

            if (answer == true)
            {
                Move(move_x, move_y);
                control_move = true;
            }
            else
            {
                if (rotate_L == true)
                {
                    Rotate(2, 0, 3, 1);
                }
                else if (rotate_R == true)
                {
                    Rotate(1, 3, 0, 2);
                }
            }

        }
        else if (delite == true)
        {
            unable = true;
            StartCoroutine(Delay(rimit, () =>
                {
                    
                    for (int re_x = 1; re_x < 21; re_x++)
                    {
                        Delite();
                    }

                    if (cubes[x, y].GetComponent<color_v2>().can_make == true)
                    {
                        if (deleting == false)
                        {
                            Gameover();
                            Rebuild();
                            cubes[x, y].GetComponent<color_v2>().can_make = false;
                        }
                    }

                }));
        }
    }

    //壁、床の判断3マス用（確認する座標×3、再生成させるか、 移動か回転か、どこに移動するか（詳しくはFloorDecision））
    void Decision_calculate3(int floor_count_x1, int floor_count_y1, int floor_count_x2, int floor_count_y2,
                            int floor_count_x3, int floor_count_y3,
                            bool create, bool move_or_rotate, int move_x, int move_y)
    {
        bool delite = create;
        bool answer = move_or_rotate;

        if (cubes[floor_count_x1, floor_count_y1].GetComponent<color_v2>().c_swich == false &&
                           cubes[floor_count_x2, floor_count_y2].GetComponent<color_v2>().c_swich == false &&
                           cubes[floor_count_x3, floor_count_y3].GetComponent<color_v2>().c_swich == false)
        {

            if (answer == true)
            {
                Move(move_x, move_y);
                control_move = true;
            }
            else
            {
                if (rotate_L == true)
                {
                    Rotate(2, 0, 3, 1);
                }
                else if (rotate_R == true)
                {
                    Rotate(1, 3, 0, 2);
                }
            }

        }
        else if (delite == true)
        {
            unable = true;
            StartCoroutine(Delay(rimit, () =>
            {
                

                for (int re_x = 1; re_x < 21; re_x++)
                {
                    Delite();
                }

                if (cubes[x, y].GetComponent<color_v2>().can_make == true)
                {
                    if (deleting == false)
                    {
                        Gameover();
                        Rebuild();
                        cubes[x, y].GetComponent<color_v2>().can_make = false;
                    }
                }

            }));
        }
    }

    //壁、床の判断2マス用（確認する座標×2、再生成させるか、 移動か回転か、どこに移動するか（詳しくはFloorDecision））
    void Decision_calculate2(int floor_count_x1, int floor_count_y1, int floor_count_x2, int floor_count_y2,
                            bool create, bool move_or_rotate, int move_x, int move_y)
    {
        bool delite = create;
        bool answer = move_or_rotate;

        if (cubes[floor_count_x1, floor_count_y1].GetComponent<color_v2>().c_swich == false &&
                           cubes[floor_count_x2, floor_count_y2].GetComponent<color_v2>().c_swich == false)
        {

            if (answer == true)
            {
                Move(move_x, move_y);
                control_move = true;
            }
            else
            {
                if (rotate_L == true)
                {
                    Rotate(2, 0, 3, 1);
                }
                else if (rotate_R == true)
                {
                    Rotate(1, 3, 0, 2);
                }
            }

        }
        else if (delite == true)
        {
            unable = true;
            StartCoroutine(Delay(rimit, () =>
            {
                

                for (int re_x = 1; re_x < 21; re_x++)
                {
                    Delite();
                }

                if(cubes[x, y].GetComponent<color_v2>().can_make == true)
                {
                    if (deleting == false)
                    {
                        Gameover();
                        Rebuild();
                        cubes[x, y].GetComponent<color_v2>().can_make = false;
                    }
                }

            }));
        }
    }

    //壁、床の判断1マス用 （確認する座標、再生成させるか、移動か回転か、どこに移動するか（詳しくはFloorDecision））
    void Decision_calculate1(int floor_count_x1, int floor_count_y1, bool create, bool move_or_rotate, int move_x, int move_y)
    {
        bool delite = create;
        bool answer = move_or_rotate;

        if (cubes[floor_count_x1, floor_count_y1].GetComponent<color_v2>().c_swich == false)
        {
            if (answer == true)
            {
                Move(move_x, move_y);
                control_move = true;
            }
            else
            {
                if (rotate_L == true)
                {
                    Rotate(2, 0, 3, 1);
                }
                else if (rotate_R == true)
                {
                    Rotate(1, 3, 0, 2);
                }
            }
        }
        else if (delite == true)
        {
            unable = true;
            StartCoroutine(Delay(rimit, () =>
            {

                Delite();

                if (cubes[x, y].GetComponent<color_v2>().can_make == true)
                    {
                        if (deleting == false)
                        {
                            Gameover();
                            Rebuild();
                            cubes[x, y].GetComponent<color_v2>().can_make = false;
                        }
                    }

            }));
        }
    }

    //落下判定用, 末尾で移動、回転制御（０は落下、１は左、２は右、３は左回転、４は右回転）
    void FloorDecision(int floor_x, int floor_y, int checker)
    {

        switch (cubes[floor_x, floor_y].GetComponent<color_v2>().cube_type)
        {
            //I
            case 1:

                switch (cubes[floor_x, floor_y].GetComponent<color_v2>().pos_type)
                {
                    //0°
                    case 0:

                        switch (checker)
                        {
                            //落下
                            case 1:
                                Decision_calculate4(floor_x - 2, floor_y - 1,
                                                    floor_x - 1, floor_y - 1,
                                                        floor_x, floor_y - 1,
                                                    floor_x + 1, floor_y - 1,
                                                    true, true, 0, -1);
                                break;

                            //左
                            case 2:

                                Decision_calculate1(floor_x - 3, floor_y, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate1(floor_x + 2, floor_y, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x, floor_y + 1, floor_x, floor_y - 1,
                                                    floor_x, floor_y - 2, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x, floor_y + 2, floor_x, floor_y + 1,
                                                    floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //90°
                    case 1:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate1(floor_x, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate4(floor_x - 1, floor_y + 2, floor_x - 1, floor_y + 1,
                                                    floor_x - 1, floor_y, floor_x - 1, floor_y - 1,
                                                    false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate4(floor_x + 1, floor_y + 2, floor_x + 1, floor_y + 1,
                                                    floor_x + 1, floor_y, floor_x + 1, floor_y - 1,
                                                    false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x - 2, floor_y, floor_x - 1, floor_y,
                                                    floor_x + 1, floor_y, false, false, 0, 0);

                                break;
                            //右回転
                            case 5:

                                Decision_calculate3(floor_x - 1, floor_y, floor_x + 1, floor_y,
                                                    floor_x + 2, floor_y, false, false, 0, 0);

                                break;
                        }

                        break;

                    //-90°
                    case 2:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate1(floor_x, floor_y - 3, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate4(floor_x - 1, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x - 1, floor_y - 1, floor_x - 1, floor_y - 2,
                                                    false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate4(floor_x + 1, floor_y + 1, floor_x + 1, floor_y,
                                                    floor_x + 1, floor_y - 1, floor_x + 1, floor_y - 2,
                                                    false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x - 1, floor_y, floor_x + 1, floor_y,
                                                    floor_x + 2, floor_y, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x - 2, floor_y, floor_x - 1, floor_y,
                                                    floor_x + 1, floor_y, false, false, 0, 0);

                                break;
                        }

                        break;

                    //180°
                    case 3:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate4(floor_x - 1, floor_y - 1,
                                               floor_x, floor_y - 1,
                                               floor_x + 1, floor_y - 1,
                                               floor_x + 2, floor_y - 1,
                                               true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate1(floor_x - 2, floor_y, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate1(floor_x + 3, floor_y, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x, floor_y + 2, floor_x, floor_y + 1,
                                                    floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x, floor_y + 1, floor_x, floor_y - 1,
                                                    floor_x, floor_y - 2, false, false, 0, 0);

                                break;
                        }

                        break;
                }

                break;

            //O
            case 2:

                switch (cubes[floor_x, floor_y].GetComponent<color_v2>().pos_type)
                {
                    //0°
                    case 0:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x, floor_y - 1, floor_x + 1, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 1, floor_y + 1, floor_x - 1, floor_y, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 2, floor_y + 1, floor_x + 2, floor_y, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x - 1, floor_y + 1, floor_x - 1, floor_y, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x, floor_y - 1, floor_x + 1, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //90°
                    case 1:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x, floor_y - 2, floor_x + 1, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 1, floor_y, floor_x - 1, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 2, floor_y, floor_x + 2, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x, floor_y + 1, floor_x + 1, floor_y + 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x - 1, floor_y, floor_x - 1, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //-90°
                    case 2:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x - 1, floor_y - 1, floor_x, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 2, floor_y + 1, floor_x - 2, floor_y, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 1, floor_y + 1, floor_x + 1, floor_y, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x - 1, floor_y - 1, floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x + 1, floor_y + 1, floor_x + 1, floor_y, false, false, 0, 0);

                                break;

                        }

                        break;

                    //180°
                    case 3:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x - 1, floor_y - 2, floor_x, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 2, floor_y, floor_x - 2, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x - 1, floor_y + 1, floor_x, floor_y + 1, false, false, 0, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x + 1, floor_y, floor_x + 1, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x - 1, floor_y + 1, floor_x, floor_y + 1, false, false, 0, 0);

                                break;

                        }

                        break;
                }

                break;

            //S
            case 3:

                switch (cubes[floor_x, floor_y].GetComponent<color_v2>().pos_type)
                {
                    //0°
                    case 0:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate3(floor_x - 1, floor_y - 1, floor_x, floor_y - 1,
                                                    floor_x + 1, floor_y, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 1, floor_y + 1, floor_x - 2, floor_y, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 2, floor_y + 1, floor_x + 1, floor_y, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x - 1, floor_y + 1, floor_x, floor_y - 1, false, false, 0, 0);

                                break;
                            //右回転
                            case 5:

                                Decision_calculate2(floor_x + 1, floor_y, floor_x + 1, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //90°
                    case 1:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x, floor_y - 1, floor_x + 1, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate3(floor_x + 1, floor_y + 1, floor_x + 2, floor_y,
                                                    floor_x + 2, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x + 1, floor_y + 1, floor_x - 1, floor_y, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x - 1, floor_y - 1, floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //-90°
                    case 2:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x - 1, floor_y - 1, floor_x, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate3(floor_x - 2, floor_y + 1, floor_x - 2, floor_y,
                                                    floor_x - 1, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate3(floor_x, floor_y + 1, floor_x + 1, floor_y,
                                                    floor_x + 1, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x + 1, floor_y, floor_x - 1, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x, floor_y + 1, floor_x + 1, floor_y + 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //180°
                    case 3:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate3(floor_x - 1, floor_y - 2, floor_x, floor_y - 2,
                                                    floor_x + 1, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 1, floor_y, floor_x - 2, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 2, floor_y, floor_x + 1, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x, floor_y + 1, floor_x + 1, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x - 1, floor_y + 1, floor_x - 1, floor_y, false, false, 0, 0);

                                break;

                        }

                        break;
                }

                break;

            //Z
            case 4:

                switch (cubes[floor_x, floor_y].GetComponent<color_v2>().pos_type)
                {
                    //0°
                    case 0:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate3(floor_x - 1, floor_y, floor_x, floor_y - 1,
                                                    floor_x + 1, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 2, floor_y + 1, floor_x - 1, floor_y, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 1, floor_y + 1, floor_x + 2, floor_y, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x - 1, floor_y, floor_x - 1, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x + 1, floor_y + 1, floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //90°
                    case 1:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x, floor_y - 2, floor_x + 1, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate3(floor_x, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x - 1, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate3(floor_x + 2, floor_y + 1, floor_x + 2, floor_y,
                                                    floor_x + 1, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x - 1, floor_y + 1, floor_x, floor_y + 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x - 1, floor_y, floor_x + 1, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //-90°
                    case 2:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x - 1, floor_y - 2, floor_x, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x - 2, floor_y,
                                                    floor_x - 2, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate3(floor_x + 1, floor_y + 1, floor_x + 1, floor_y,
                                                    floor_x, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x, floor_y - 1, floor_x + 1, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x - 1, floor_y + 1, floor_x + 1, floor_y, false, false, 0, 0);

                                break;

                        }

                        break;

                    //180°
                    case 3:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate3(floor_x - 1, floor_y - 1, floor_x, floor_y - 2,
                                                    floor_x + 1, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 2, floor_y, floor_x - 1, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 1, floor_y, floor_x + 2, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate2(floor_x + 1, floor_y + 1, floor_x + 1, floor_y, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate2(floor_x, floor_y + 1, floor_x - 1, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;
                }

                break;

            //J
            case 5:

                switch (cubes[floor_x, floor_y].GetComponent<color_v2>().pos_type)
                {
                    //0°
                    case 0:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate3(floor_x - 1, floor_y - 1, floor_x, floor_y - 1,
                                                    floor_x + 1, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 2, floor_y + 1, floor_x - 2, floor_y, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x, floor_y + 1, floor_x + 2, floor_y, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x, floor_y + 1, floor_x - 1, floor_y - 1,
                                                    floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x, floor_y + 1, floor_x + 1, floor_y + 1,
                                                    floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //90°
                    case 1:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x, floor_y - 2, floor_x + 1, floor_y, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x - 1, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate3(floor_x + 2, floor_y + 1, floor_x + 1, floor_y,
                                                    floor_x + 1, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x + 1, floor_y, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x - 1, floor_y, floor_x + 1, floor_y,
                                                    floor_x + 1, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //-90°
                    case 2:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x - 1, floor_y - 2, floor_x, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x - 2, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate3(floor_x + 1, floor_y + 1, floor_x + 1, floor_y,
                                                    floor_x + 1, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x - 1, floor_y, floor_x + 1, floor_y,
                                                    floor_x + 1, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x + 1, floor_y, false, false, 0, 0);

                                break;

                        }

                        break;

                    //180°
                    case 3:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate3(floor_x - 1, floor_y - 1, floor_x, floor_y - 1,
                                                    floor_x + 1, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 2, floor_y, floor_x, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 2, floor_y, floor_x + 2, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x, floor_y + 1, floor_x + 1, floor_y + 1,
                                                    floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x, floor_y + 1, floor_x - 1, floor_y - 1,
                                                    floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;
                }

                break;

            //L
            case 6:

                switch (cubes[floor_x, floor_y].GetComponent<color_v2>().pos_type)
                {
                    //0°
                    case 0:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate3(floor_x - 1, floor_y - 1, floor_x, floor_y - 1,
                                                    floor_x + 1, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 2, floor_y, floor_x, floor_y + 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 2, floor_y + 1, floor_x + 2, floor_y, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x, floor_y + 1,
                                                    floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x, floor_y + 1, floor_x, floor_y - 1,
                                                    floor_x + 1, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //90°
                    case 1:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x, floor_y - 2, floor_x + 1, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x - 1, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate3(floor_x + 1, floor_y + 1, floor_x + 1, floor_y,
                                                    floor_x + 2, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x + 1, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x + 1, floor_y, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x - 1, floor_y, floor_x + 1, floor_y,
                                                    floor_x - 1, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //-90°
                    case 2:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x - 1, floor_y, floor_x, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate3(floor_x - 2, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x - 1, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate3(floor_x + 1, floor_y + 1, floor_x + 1, floor_y,
                                                    floor_x + 1, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x - 1, floor_y, floor_x + 1, floor_y,
                                                    floor_x - 1, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x + 1, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x + 1, floor_y, false, false, 0, 0);

                                break;

                        }

                        break;

                    //180°
                    case 3:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate3(floor_x - 1, floor_y - 2, floor_x, floor_y - 1,
                                                    floor_x + 1, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 2, floor_y, floor_x - 2, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 2, floor_y, floor_x, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate3(floor_x, floor_y + 1, floor_x, floor_y - 1,
                                                    floor_x + 1, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x, floor_y + 1,
                                                    floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;
                }

                break;

            //T
            case 7:

                switch (cubes[floor_x, floor_y].GetComponent<color_v2>().pos_type)
                {
                    //0°
                    case 0:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate3(floor_x - 1, floor_y - 1, floor_x, floor_y - 1,
                                                    floor_x + 1, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 1, floor_y + 1, floor_x - 2, floor_y, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 1, floor_y - 1, floor_x + 2, floor_y, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate1(floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate1(floor_x, floor_y - 1, false, false, 0, 0);

                                break;

                        }

                        break;

                    //90°
                    case 1:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x, floor_y - 2, floor_x + 1, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x - 1, floor_y,
                                                    floor_x + 1, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate3(floor_x + 1, floor_y + 1, floor_x + 2, floor_y,
                                                    floor_x + 1, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate1(floor_x - 1, floor_y, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate1(floor_x - 1, floor_y, false, false, 0, 0);

                                break;

                        }

                        break;

                    //-90°
                    case 2:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate2(floor_x - 1, floor_y - 1, floor_x, floor_y - 2, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate3(floor_x - 1, floor_y + 1, floor_x - 2, floor_y,
                                                    floor_x - 1, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate3(floor_x + 1, floor_y + 1, floor_x + 1, floor_y,
                                                    floor_x + 1, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate1(floor_x + 1, floor_y, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate1(floor_x + 1, floor_y, false, false, 0, 0);

                                break;

                        }

                        break;

                    //180°
                    case 3:

                        switch (checker)
                        {
                            //落下
                            case 1:

                                Decision_calculate3(floor_x - 1, floor_y - 1, floor_x, floor_y - 2,
                                                    floor_x + 1, floor_y - 1, true, true, 0, -1);

                                break;

                            //左
                            case 2:

                                Decision_calculate2(floor_x - 2, floor_y, floor_x - 1, floor_y - 1, false, true, -1, 0);

                                break;

                            //右
                            case 3:

                                Decision_calculate2(floor_x + 2, floor_y, floor_x + 1, floor_y - 1, false, true, +1, 0);

                                break;

                            //左回転
                            case 4:

                                Decision_calculate1(floor_x, floor_y + 1, false, false, 0, 0);

                                break;

                            //右回転
                            case 5:

                                Decision_calculate1(floor_x, floor_y + 1, false, false, 0, 0);

                                break;

                        }

                        break;
                }

                break;
        }
    }

    //移動(左なら -1, 右なら +1, 落下は0, -1)
    void Move(int movement, int down)
    {
        BlockType(x, y, false);
        x = x + movement;
        y = y + down;
        cubes[x, y].GetComponent<color_v2>().control_cube = true;
        cubes[x, y].GetComponent<color_v2>().pos_type = cubes[x - movement, y - down].GetComponent<color_v2>().pos_type;
        cubes[x, y].GetComponent<color_v2>().cube_type = cubes[x - movement, y - down].GetComponent<color_v2>().cube_type;


        cubes[x - movement, y - down].GetComponent<color_v2>().control_cube = false;
        cubes[x - movement, y - down].GetComponent<color_v2>().pos_type = 0;
        cubes[x - movement, y - down].GetComponent<color_v2>().cube_type = 0;

        BlockType(x, y, true);
    }

    //回転
    void Rotate(int pattern1, int pattern2, int pattern_3, int pattern_4)
    {
        BlockType(x, y, false);

        switch (cubes[x, y].GetComponent<color_v2>().pos_type)
        {
            case 0:
                cubes[x, y].GetComponent<color_v2>().pos_type = pattern1;
                break;

            case 1:
                cubes[x, y].GetComponent<color_v2>().pos_type = pattern2;
                break;

            case 2:
                cubes[x, y].GetComponent<color_v2>().pos_type = pattern_3;
                break;

            case 3:
                cubes[x, y].GetComponent<color_v2>().pos_type = pattern_4;
                break;
        }

        BlockType(x, y, true);
    }

    //列消し用
    void Delite()
    {
        control_delite = false;
        deleting = true;

        for (int re_x = 1; re_x < 21; re_x++)
        {
            Delite_memory(re_x);
        }

        //列の削除と下へ落とす挙動

        if (delite_check == true)
        {
            for (int numbers = 0; numbers < delite_number.Count; numbers++)
            {
                int dummy = delite_number[numbers];

                for (int c_x = 2; c_x < 12; c_x++)
                {
                    cubes[c_x, dummy].GetComponent<color_v2>().c_swich = false;

                    for (int c_y = dummy; c_y < 21; c_y++)
                    {
                        cubes[c_x, c_y].GetComponent<color_v2>().c_swich = cubes[c_x, c_y + 1].GetComponent<color_v2>().c_swich;
                    }
                }

                switch(delite_number.Count - 1)
                {
                    case 0:

                        delite_number.Clear();
                        check_count = 0;

                        break;

                    case 1:

                        if(check_count == 1)
                        {
                            
                            delite_number.Clear();
                            check_count = 0;
                        }
                        else
                        {
                            delite_number[numbers + 1] = delite_number[numbers + 1] - 1;
                            check_count += 1;
                        }

                        break;

                    case 2:

                        if (check_count == 2)
                        {
                            delite_number.Clear();
                            check_count = 0;
                        }
                        else
                        {
                            delite_number[numbers + 1] = delite_number[numbers + 1] - 1;
                            if (check_count == 0)
                            {
                                delite_number[numbers + 2] = delite_number[numbers + 2] - 1;
                            }
                            check_count += 1;
                        }

                        break;

                    case 3:

                        if (check_count == 3)
                        {
                            delite_number.Clear();
                            check_count = 0;
                        }
                        else
                        {
                            delite_number[numbers + 1] = delite_number[numbers + 1] - 1;
                            if (check_count == 1)
                            {
                                delite_number[numbers + 2] = delite_number[numbers + 2] - 1;
                            }

                            if (check_count == 0)
                            {
                                delite_number[numbers + 2] = delite_number[numbers + 2] - 1;
                                delite_number[numbers + 3] = delite_number[numbers + 3] - 1;
                            }
                            check_count += 1;
                        }

                        break;
                }
            }

            
            delite_check = false;
        }

        
        
        control_delite = true;
        deleting = false;

    }

    void Delite_memory(int delite_memory_y)
    {
        if (cubes[2, delite_memory_y].GetComponent<color_v2>().c_swich == true &&
            cubes[3, delite_memory_y].GetComponent<color_v2>().c_swich == true &&
            cubes[4, delite_memory_y].GetComponent<color_v2>().c_swich == true &&
            cubes[5, delite_memory_y].GetComponent<color_v2>().c_swich == true &&
            cubes[6, delite_memory_y].GetComponent<color_v2>().c_swich == true &&
            cubes[7, delite_memory_y].GetComponent<color_v2>().c_swich == true &&
            cubes[8, delite_memory_y].GetComponent<color_v2>().c_swich == true &&
            cubes[9, delite_memory_y].GetComponent<color_v2>().c_swich == true &&
            cubes[10, delite_memory_y].GetComponent<color_v2>().c_swich == true &&
            cubes[11, delite_memory_y].GetComponent<color_v2>().c_swich == true)
        {
            delite_check = true;
            delite_number.Add(delite_memory_y);
            level_speed -= 0.05f;
        }
    }

    //ゲームオーバー用
    void Gameover()
    {
        if (cubes[4, 20].GetComponent<color_v2>().c_swich == true |
            cubes[5, 20].GetComponent<color_v2>().c_swich == true |
            cubes[6, 20].GetComponent<color_v2>().c_swich == true |
            cubes[7, 20].GetComponent<color_v2>().c_swich == true)
        {
            for (int dx = 2; dx < 12; dx++)
            {
                for (int dy = 1; dy < 22; dy++)
                {
                    cubes[dx, dy].GetComponent<color_v2>().c_swich = false;
                }
            }

            mode = 1;
        }
    }

    //ゲームオーバー後の再開用
    void Continue()
    {
        cubes[x, y].GetComponent<color_v2>().cube_type = UnityEngine.Random.Range(1, 7);
        cubes[x, y].GetComponent<color_v2>().pos_type = 0;
        cubes[x, y].GetComponent<color_v2>().control_cube = true;

        BlockType(x, y, true);

        cubes[x, y].GetComponent<color_v2>().can_make = false;
        control_move = true;
        unable = false;
        level_speed = 0.0f;
        accele_speed = 0.0f;

        mode = 0;
    }

    //ピースを落下させる関数
    void BlockDown()
    {
        block_speed -= Time.deltaTime;
        if (block_speed <= 0.0f)
        {
            block_speed = defult_block_speed + level_speed + accele_speed;

            if (control_delite == true)
            {
                if (control_move == true)
                {
                    control_move = false;
                    //下へ下す処理
                    FloorDecision(x, y, 1);

                }
            }
        }
    }

    //操作関係
    void BlockController()
    {
        if (unable == false)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                accele_speed = -0.9f;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                accele_speed = 0.0f;
            }

            //左
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                left = true;

                FloorDecision(x, y, 2);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                left = false;
            }

            //右
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                right = true;

                FloorDecision(x, y, 3);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                right = false;
            }

            //左回転
            if (Input.GetKeyDown(KeyCode.A))
            {
                rotate_L = true;

                FloorDecision(x, y, 4);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                rotate_L = false;
            }

            //右回転
            if (Input.GetKeyDown(KeyCode.S))
            {
                rotate_R = true;

                FloorDecision(x, y, 5);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                rotate_R = false;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(text_delete == true)
                {
                    text_delete = false;
                }
                else
                {
                    text_delete = true;
                }
            }
        }
    }

    void Start()
    {
        for (int i = 0; i < 14; i++)
        {
            for (int r = 0; r < 23; r++)
            {                                    //6,19 中心？
                transform.position = new Vector3(i, r, 0);
                cubes[i, r] = Instantiate(cube, transform.position, transform.rotation);
            }
        }

        //壁生成
        for (int i = 0; i < 21; i++)
        {
            cubes[0, i].GetComponent<color_v2>().c_swich = true;
            cubes[1, i].GetComponent<color_v2>().c_swich = true;
            cubes[12, i].GetComponent<color_v2>().c_swich = true;
            cubes[13, i].GetComponent<color_v2>().c_swich = true;
        }

        ////底生成
        for (int i = 2; i < 12; i++)
        {
            cubes[i, 0].GetComponent<color_v2>().c_swich = true;
        }

        delite_check = false;

        left = false;
        right = false;
        rotate_L = false;
        rotate_R = false;

        x = reference_x;
        y = reference_y;

        control_move = true;

        control_delite = true;

        unable = false;

        mode = 0;

        cubes[x, y].GetComponent<color_v2>().cube_type = UnityEngine.Random.Range(1, 7);
        cubes[x, y].GetComponent<color_v2>().pos_type = 0;
        cubes[x, y].GetComponent<color_v2>().control_cube = true;

        BlockType(x, y, true);
    }

    private void Update()
    {
        switch(mode)
        {
            //暫定ゲーム画面
            case 0:

                GameOverTextObject.SetActive(false);
                ButtonTextObject.SetActive(false);
                ContinueTextObjct.SetActive(false);

                if (text_delete == true)
                {
                    DeleteTextObject.SetActive(true);
                    MoveTextObject.SetActive(true);
                    L_rotateTextObjct.SetActive(true);
                    R_rotateTextObjct.SetActive(true);
                }
                else
                {
                    DeleteTextObject.SetActive(false);
                    MoveTextObject.SetActive(false);
                    L_rotateTextObjct.SetActive(false);
                    R_rotateTextObjct.SetActive(false);
                }

                

                BlockDown();

                BlockController();

                break;

            //コンティニュー画面
            case 1:

                MoveTextObject.SetActive(false);
                L_rotateTextObjct.SetActive(false);
                R_rotateTextObjct.SetActive(false);
                DeleteTextObject.SetActive(false);

                GameOverTextObject.SetActive(true);
                ButtonTextObject.SetActive(true);
                ContinueTextObjct.SetActive(true);

                BlockType(x, y, false);
                cubes[x, y].GetComponent<color_v2>().control_cube = false;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Continue();
                }

                break;
        }
    }

}
