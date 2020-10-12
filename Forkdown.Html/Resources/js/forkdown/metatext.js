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
        // Header item
        let header = $(meta).parent(".fd--main").parent("article").find("> header")
        if (header.length > 0) {
          header.append(meta)
        }
        // Check item
        let help = $(meta).parent("td").parent("tr").parent("tbody").parent(".fd--checkitem")
          .find("> thead th.fd--help")
        if (help.length > 0) {
          let content = $(meta).parent(".fd--content")
          help.removeClass("fd--none").append(meta)
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