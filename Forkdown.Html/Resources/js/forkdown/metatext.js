'use strict'

class ForkdownMetaText {
  init = () => {
    let meta = cssClass => {
      let metas = $("." + cssClass)
      let icon = metas.first().children(".fd--meta-label").text()
      metas.each((i, meta) => {
        let main = $(meta).closest(".fd--main")
        if (main.parent().is("article")) {
          if (!$(meta).hasClass("fd--x"))
            return;
          main = main.parent().find("header").first()
        }
        else
          main = main.find(".fd--paragraph > p").first()
        let button = $("<span>").addClass(cssClass).addClass("fd--meta").html(icon)
        let content = $(meta).find("p").html()

        main.append(button)
        $(meta).remove();
  
        window.tippy(button.get(), {
          content: content,
          allowHTML: true,
          interactive: true,
          placement: 'bottom-start',
          theme: 'light-border',
          trigger: 'click' + (cssClass === "fd--info" ? ' mouseenter focus' : ''),
          maxWidth: 350,
        })
      })
    }
    meta("fd--x")
    meta("fd--info")   
  }
}