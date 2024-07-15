document.addEventListener("DOMContentLoaded", function () {
  const signUpButton = document.getElementById("signUp");
  const signInButton = document.getElementById("signIn");
  const container = document.getElementById("container");

  if (signUpButton && signInButton && container) {
    signUpButton.addEventListener("click", () => {
      container.classList.add("right-panel-active");
    });

    signInButton.addEventListener("click", () => {
      container.classList.remove("right-panel-active");
    });
  }
  // Button change color
  var tags = document.querySelectorAll(".tag_fiter");
  tags.forEach(function (tag) {
    tag.addEventListener("click", function () {
      if (this.classList.contains("active")) {
        this.classList.remove("active");
      } else {
        tags.forEach(function (tag) {
          tag.classList.remove("active");
        });
        this.classList.add("active");
      }
    });
  });
});

document.addEventListener("DOMContentLoaded", (event) => {
  const editableTitle = document.getElementById("editable-title");

  editableTitle.addEventListener("click", () => {
    editableTitle.setAttribute("contenteditable", "true");
    editableTitle.focus();
  });

  editableTitle.addEventListener("blur", () => {
    editableTitle.removeAttribute("contenteditable");
  });
});
