using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.2f;      //Ç½Ãæ¼ì²â¾àÀë
    public float ledgeCheckDistance = 0.4f;     //ĞüÑÂ¼ì²â¾àÀë


    public float minAgroDistance = 3f;      //×îĞ¡³ğºŞ¾àÀë
    public float maxAgroDistance = 4f;      //×î´ó³ğºŞ¾àÀë

    //¹¥»÷¼ì²â¾àÀë
    public float closeRangeActionDistance = 1f;
    public float longRangeActionDistance = 5f;

    public LayerMask whatIsGround;      //µØÃæÍ¼²ã
    public LayerMask whatIsPlayer;      //½ÇÉ«Í¼²ã
}
