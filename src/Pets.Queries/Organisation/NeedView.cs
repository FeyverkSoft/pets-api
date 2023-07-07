namespace Pets.Queries.Organisation;

using System.Collections.Generic;

using Types;

   /// <summary>
   /// 
   /// </summary>
   /// <param name="ImgsLink"> Ссылка на фотографию материала</param>
   /// <param name="MdBody">Тело в markdown</param>
   /// <param name="State"> Видимость заказа</param>
public sealed record NeedView(IEnumerable<String?> ImgsLink, String? MdBody, NeedState State);