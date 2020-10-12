'use strict'

class ForkdownMetaText {
  init = () => {
    let meta = cssClass => {
      let metas = $(".fd--paragraph." + cssClass)
      metas.each((i, meta) => {
        let button = $(meta).find(".fd--meta-label")
        let content = $(meta).find("p")
        let html = content.html()

        // List item
        let parent = $(meta).parent("li").find(".fd--paragraph").first()
        if (parent.length > 0) {
          parent.append(meta)
        }
        // Check item
        else {
          let content = $(meta).parent(".fd--content")
          parent = content.parent("tr").parent("tbody").parent(".fd--checkitem").find("> thead th.fd--help")
          if (parent.length > 0) {
            parent.removeClass("fd--none").append(meta)
          }
          if (content.children().length < 1)
            content.addClass("fd--none")
        }
        $(meta).replaceWith(button)

        // Collapse to button
        window.tippy(button.get(), {
          content: html,
          allowHTML: true,
          interactive: true,
          placement: 'bottom-start',
          theme: 'light-border',
          trigger: 'click' + (cssClass === "fd--info" ? ' mouseenter focus' : ''),
          maxWidth: 350,
        })
      })
    }
    meta("fd--help")
    meta("fd--info")
  }
}