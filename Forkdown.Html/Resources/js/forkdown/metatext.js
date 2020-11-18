'use strict'

class ForkdownMetaText {
  init = () => {
    let meta = cssClass => {
      let metaTexts = $("." + cssClass)
      metaTexts.each((i, metaText) => {
        let container = $(metaText).parent(".fd--content")
        let button = $(metaText).find("> .fd--meta-label")
        let content = $(metaText)
        content[0].removeChild(button[0])
        let html = content[0].outerHTML

        // Check item
        let item = container.parent(".fd--item")
        let header = item.find("> header")
        if (header.length > 0) {
          let metaNode = header.find(".fd--meta")
          if (metaNode.length < 1) {
            let spacer = $("> .fd--spacer", header)
            metaNode = $("<div>").addClass("fd--meta")
            metaNode.insertBefore(spacer)
          }
          metaNode.append(metaText)
          $(metaText).replaceWith(button)

          if (container.children().length < 1)
            container.remove()

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
        }
      })
    }
    meta("fd--help")
    meta("fd--info")
  }
}