using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

   public interface IDamageable
    {

        void TakeDamage(int damage, Vector2 direction, float speed);
    }
