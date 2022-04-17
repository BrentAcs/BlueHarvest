namespace BlueHarvest.ConSoul.Common;

public static class SpectreConsoleExtensions
{
   public static Markup ToMarkup<T>(this T? obj, int colWidth, Color? color = null, Overflow overflow = Overflow.Ellipsis) where T : notnull
   {
      if (obj is null)
         throw new ArgumentNullException(nameof(obj));

      string text = obj?.ToString();
      if (text.Length > colWidth)
      {
         text = overflow == Overflow.Ellipsis ? $"{text[ ..(colWidth - 1) ]}…" : text[ ..colWidth ];
      }

      string markupText = color.HasValue ? $"[{color.Value}]{text}[/]" : text;

      return new Markup(markupText);
   }

   public static Markup ToMarkup(this string? text, int maxWidth, Style? style = null, Overflow overflow = Overflow.Ellipsis)
   {
      if (text is null)
         throw new ArgumentNullException(nameof(text));

      if (text.Length > maxWidth)
      {
         text = overflow == Overflow.Ellipsis ? $"{text[ ..(maxWidth - 1) ]}…" : text[ ..maxWidth ];
      }

      return new Markup(text, style);
   }
}
