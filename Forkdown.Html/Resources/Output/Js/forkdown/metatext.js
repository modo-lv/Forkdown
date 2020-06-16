'use strict'

class ForkdownMetaText {
  init = () => {

    /*
    // Find all X paragraphs
    this.xs = $("p.fd--x")

    // Find the symbol
    this.label = this.xs.children().first().text()

    this.xs.each((i, x) => {
      $(x).children().first().remove(); // Remove the icon from the text
      $(x).hide()
      $(x).addClass("fd--ready")

      let button = $("<p>" + this.label + "</p>").addClass("fd--x-button")
      button.on("click", (e) => {
        e.stopPropagation();
        ForkdownMetaText.hideAll();
        console.log(e.target, $(e.target).offset(), $(e.target).position(), $(e.target).outerHeight(), )
        let top = $(e.target).position().top + $(e.target).outerHeight(true) + $("main").scrollTop()
        let left = $(e.target).closest("article").position().left;
        let width = $(e.target).closest("article").outerWidth()
        $(x).show().css({
          "top": top + "px",
          "left": left + "px",
          "width": width + "px"
        }).addClass("fd--open");
      })

      if ($(x).parent().hasClass("fd--content"))
        $(x).closest(".fd--content").find("p:first-child").prepend(button)
      else if ($(x).parent().is("li"))
        $(x).parent().children("p:first-child").prepend(button);
      else if ($(x).parent().is("div[role='main']"))
        $(x).closest("article").find("header > *").append(button);
      else
        $(x).parent().append(button)
    })

    $("body").on("click", ForkdownMetaText.hideAll)
    $(document).on("keyup", (e) => {
      if (e.keyCode == 27) // Escape
        ForkdownMetaText.hideAll()
    })
    */
  }

  static hideAll() {
    //$("p.fd--x").removeClass("fd--open").hide()
  }
}