using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FaceScript : MonoBehaviour
{
    public List<FaceSprite> Emotions;
    public SpriteRenderer Eyes;
    public SpriteRenderer Mouth;

    public void SwitchFace(Enums.FaceType faceType)
    {
        var x = Emotions.SingleOrDefault(o => o.FaceType == faceType);
        if (x == null)
            return;
        
        Eyes.sprite = x.Eye;
        Mouth.sprite = x.Mouth;
    }

    [Serializable]
    public class FaceSprite
    {
        public Sprite Eye;
        public Sprite Mouth;
        public Enums.FaceType FaceType;
    }
}