'use strict'

class ForkdownMetaText {
  init = () => {
    let meta = cssClass => {
      let metas = $("." + cssClass)
      let icon = metas.first().children(".fd--meta-label").text()
      metas.each((i, meta) => {
        let main = $(meta).closest("article").children("header").first().find("p, h1, h2, h3, h4, h5, h6").first()
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