'use strict'

class ForkdownMetaText {
  init = () => {
    let meta = cssClass => {
      let metas = $(".fd--paragraph." + cssClass)
      metas.each((i, meta) => {
        let button = $(meta).find(".fd--meta-label")
        let content = $(meta).find("p")
        let html = content.html()

        // Move help button when in list item
        let parent = $(meta).parent("li").find(".fd--paragraph").first()
        if (parent.length > 0) {
          parent.append(meta)
        }
        // Move info button when in checkitem
        else if (cssClass === "fd--info") {
          parent = $(meta).parent(".fd--content").parent(".fd--checkitem").find("header > .fd--help")
          if (parent.length > 0)
            parent.append(meta)
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