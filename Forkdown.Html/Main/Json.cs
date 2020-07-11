using System;
using Newtonsoft.Json;

namespace Forkdown.Html.Main {
  public static class Json {
    public static String Serialize(Object? obj) => JsonConvert.SerializeObject(obj);
  }
}
