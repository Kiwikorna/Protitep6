using System.Collections.Generic;


// Компаратор для HashSet<int>
   public class HashSetComparer : IEqualityComparer<HashSet<int>>
   {
      private const int HasCodeMultiplier = 31;
      public bool Equals(HashSet<int> x, HashSet<int> y)
      {
         return x.SetEquals(y);
      }

      public int GetHashCode(HashSet<int> obj)
      {
         int hash = 19;
         foreach (var item in obj)
         {
            hash = hash * HasCodeMultiplier + item.GetHashCode();
         }
         return hash;
      }
   }
