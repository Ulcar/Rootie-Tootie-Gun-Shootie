using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class AttackGenerator : Attack
{
    [SerializeField]
    AnimationCurve curve;

    [SerializeField]
    AnimationCurve rotationCurve;

    [SerializeField]
    List<AnimationCurve> Multipliers;
    //TODO: make list of basebullets instead of sprites
    [SerializeField]
    List<Bullet> baseBullets;

    [SerializeField]
    AnimationCurve timeBetweenBullets;

    [SerializeField]
    List<SpriteData> sprites;

    //Direction is in degrees
    [SerializeField]
    AnimationCurve directionCurve;

    int currentIndex = 0;

    [SerializeField]
    bool NoRotation = false;



    private void Awake()
    {
    }
    //Calculates all bullet positions at once
    public override List<Bullet> DoAttack(int rotationOffset)
    {
        List<Bullet> bullets = base.DoAttack(rotationOffset);
        Bullet baseBullet = baseBullets[0];
        foreach (Bullet bul in baseBullets)
        {
            if ((currentIndex % bul.bulletIndex == 0))
            {
                baseBullet = bul;
            }
        }
        Bullet bullet = new Bullet(baseBullet.MovementSpeed, Vector3.right, baseBullet.bulletSprite, baseBullet.lifeTime);
        bullets.Add(bullet);
        float f = curve.Evaluate(0);
        bullet.spawnTime = timeBetweenBullets.Evaluate(0);
        foreach (AnimationCurve currentCurve in Multipliers)
        {
            f *= currentCurve.Evaluate(0);
        }
        SpawnPosition(rotationCurve.Evaluate(0) + rotationOffset, bullet, f, baseBullet.Damage);
        for (int i = 1; i <= bulletAmount - 1; i++)
        {
            bullet = new Bullet(1, Vector3.right, baseBullet.bulletSprite, baseBullet.lifeTime);
            bullets.Add(bullet);
            f = curve.Evaluate(i % (curve[curve.length - 1].time + 1));
            //    f = curve.Evaluate((float)i / bulletAmount);
            foreach (AnimationCurve currentCurve in Multipliers)
            {
                //bullet based on value between 0 and 1e                //f *= currentCurve.Evaluate((float)i / bulletAmount);
                f *= currentCurve.Evaluate(i % (currentCurve[currentCurve.length - 1].time + 1));
            }
            bullet.spawnTime = timeBetweenBullets.Evaluate(i % (timeBetweenBullets[timeBetweenBullets.length - 1].time + 1));
            bullet.direction = Quaternion.Euler(0, 0, directionCurve.Evaluate(i % (directionCurve[directionCurve.length - 1].time + 1))) * Vector2.right;
 
            foreach (SpriteData sprite in sprites)
            {
                if ((i % sprite.spriteNumber == 0))
                {
                    
                    bullet.bulletSprite = sprite.sprite;
                    break;
                }
            }
            //  bullet.spawnTime =   timeBetweenBullets.Evaluate((float)i / bulletAmount);
            SpawnPosition(rotationCurve.Evaluate(i % (rotationCurve[rotationCurve.length - 1].time + 1)) + rotationOffset, bullet, f, baseBullet.Damage);
            // SpawnPosition(rotationCurve.Evaluate((float)i / bulletAmount) + rotationOffset, bullet, f);
        }
        return bullets;

    }
    //calculates bullets one at a time
    public override Bullet SingleBullet(int rotationOffset)
    {
        if (NoRotation)
        {
            rotationOffset = 0;
        }
        Bullet baseBullet = baseBullets[0];
        foreach (Bullet bul in baseBullets)
        {
            if (bul.bulletIndex == 0)
            {
                bul.bulletIndex = 1;
            }
            if ((currentIndex % bul.bulletIndex == 0))
            {
                baseBullet = bul;
            }
        }
        Bullet bullet = new Bullet(baseBullet.MovementSpeed, Vector3.right, baseBullet.bulletSprite, baseBullet.lifeTime, baseBullet.childBullets);
      float  f = curve.Evaluate(currentIndex % (curve[curve.length - 1].time + 1));
        //    f = curve.Evaluate((float)i / bulletAmount);
        foreach (AnimationCurve currentCurve in Multipliers)
        {
            //bullet based on value between 0 and 1
            //f *= currentCurve.Evaluate((float)i / bulletAmount);
            f *= currentCurve.Evaluate(currentIndex % (currentCurve[currentCurve.length - 1].time + 1));
        }
        bullet.spawnTime = timeBetweenBullets.Evaluate(currentIndex % (timeBetweenBullets[timeBetweenBullets.length - 1].time + 1));
        bullet.direction = Quaternion.Euler(0, 0, directionCurve.Evaluate(currentIndex % (directionCurve[directionCurve.length - 1].time + 1))) * Vector2.right;

        foreach (SpriteData sprite in sprites)
        {
            if ((currentIndex % sprite.spriteNumber == 0))
            {

                bullet.bulletSprite = sprite.sprite;
                break;
            }
        }
        SpawnPosition(rotationCurve.Evaluate(currentIndex % (rotationCurve[rotationCurve.length - 1].time + 1)) + rotationOffset, bullet, f, baseBullet.Damage);

        currentIndex++;
        if (currentIndex >= bulletAmount)
        {
            currentIndex = 0;
        }
        return bullet;
    }


    void Spawnbullet(float x, float y, float rotation, int damage, Bullet bullet)
    {
        var bulletposition = new Vector3(x, y, 0);
        bullet.Position = bulletposition;
        bullet.Rotation = Quaternion.Euler(0, 0, rotation);
        bullet.Damage = damage;


    }

    void SpawnPosition(float rotation, Bullet bullet, float r, int damage)
    {
        float xposition = Mathf.Cos((rotation * Mathf.PI) / 180) * r;
        float yposition = Mathf.Sin((rotation * Mathf.PI) / 180) * r;
      //  Debug.Log(r);
        Spawnbullet(xposition, yposition, rotation, damage, bullet);
    }
    [System.Serializable]
    class SpriteData:IComparer<SpriteData>
    {
       public Sprite sprite;
      public  int spriteNumber;

        public int Compare(SpriteData x, SpriteData y)
        {
            return x.spriteNumber.CompareTo(y.spriteNumber);
        }
    }
   
}
