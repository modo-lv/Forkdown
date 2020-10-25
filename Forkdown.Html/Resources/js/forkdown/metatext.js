'use strict'

class ForkdownMetaText {
  init = () => {
    let meta = cssClass => {
      let metaTexts = $(".fd--paragraph." + cssClass)
      metaTexts.each((i, metaText) => {
        let button = $(metaText).find(".fd--meta-label")
        let html = $(metaText).find("p").html()

        // Check item
        let content = $(metaText).parent(".fd--content")
        let header = content.parent(".fd--item")
          .find("> header")
        if (header.length > 0) {
          let metaNode = header.find(".fd--meta")
          if (metaNode.length < 1) {
            let placement = header.parent(".fd--item").hasClass("fd--is-heading")
              ? $("> .fd--spacer", header)
              : $("> .fd--title", header)
            metaNode = $("<div>").addClass("fd--meta").insertAfter(placement)
          }
          metaNode.append(metaText)
          $(metaText).replaceWith(button)

          if (content.children().length < 1)
            content.remove()

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