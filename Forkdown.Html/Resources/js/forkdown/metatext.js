'use strict'

class ForkdownMetaText {
  init = () => {
    let meta = cssClass => {
      let metas = $(".fd--paragraph." + cssClass)
      metas.each((i, meta) => {
        let button = $(meta).find(".fd--meta-label")
        let content = $(meta).find("p")
        let html = content.html()

        // Check item
        let header = $(meta).parent(".fd--content").parent(".fd--item")
          .find("> header")
        if (header.length > 0) {
          let help = header.find("> .fd--meta")
          if (help.length < 1)
            help = $("<div>").addClass("fd--meta").appendTo(header)
          let content = $(meta).parent(".fd--content")
          help.append(meta)
          if (content.children().length < 1)
            content.remove()
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