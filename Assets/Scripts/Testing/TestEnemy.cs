using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public enum EnemyType
    {
        ゴブリン,
        スライム,
        ドラゴン,
        オーク,
    }

    public EnemyType enemyType;

    public int hp;
    public int attack;
    public int defense;

    // ドラゴンだけが持つ特別なプロパティ
    public int fireBreath;

    // ゴブリンだけが持つ特別なプロパティ
    public int poison;

    // オークだけが持つ特別なプロパティ
    public int berserk;

    // スライムだけが持つ特別なプロパティ
    public int split;
}
