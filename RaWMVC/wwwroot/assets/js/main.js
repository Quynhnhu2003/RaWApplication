// Back to top
window.addEventListener("scroll", function () {
  var backToTopButton = document.getElementById("backToTop");
  if (window.scrollY > 300) {
    backToTopButton.style.display = "inline-block"; // Show button when user scrolls down more than 300px
  } else {
    backToTopButton.style.display = "none"; // Hide button when user doesn't scroll down more than 300px
  }
});

// Counter
var counta = 0;

$(window).scroll(function (e) {
  /* Onscroll number counter */
  var statisticNumbers = $(".single-count");
  if (statisticNumbers.length) {
    var oTop = statisticNumbers.offset().top - window.innerHeight;
    if (counta == 0 && $(window).scrollTop() > oTop) {
      $(".count").each(function () {
        var $this = $(this),
          countTo = $this.attr("data-count");
        $({
          countNum: $this.text(),
        }).animate(
          {
            countNum: countTo,
          },

          {
            duration: 2000,
            easing: "swing",
            step: function () {
              $this.text(Math.floor(this.countNum));
            },
            complete: function () {
              $this.text(this.countNum);
            },
          }
        );
      });
      counta = 1;
    }
  }
});

// Slider recommends
document.addEventListener("DOMContentLoaded", function () {
  let next = document.querySelector(".next");
  let prev = document.querySelector(".prev");

  if (next && prev) {
    next.addEventListener("click", function () {
      let items = document.querySelectorAll(".item");
      document.querySelector(".slide").appendChild(items[0]);
    });

    prev.addEventListener("click", function () {
      let items = document.querySelectorAll(".item");
      document.querySelector(".slide").prepend(items[items.length - 1]);
    });
  }
});
// Chat section (Canh sua ngay 27/7)
//document
//  .querySelector(".chat[data-chat='person2']")
//  .classList.add("active-chat");
//document.querySelector(".person[data-chat='person2']").classList.add("active");
let chatPerson2 = document
    .querySelector(".chat[data-chat='person2']");
if (chatPerson2) {
    chatPerson2.classList.add("active-chat");
}

let personPerson2 = document.querySelector(".person[data-chat='person2']");
if (personPerson2) {
    personPerson2.classList.add("active");
}


let friends = {
  list: document.querySelector("ul.people"),
  all: document.querySelectorAll(".left .person"),
  name: "",
};

let chat = {
  container: document.querySelector(".right"),
  current: null,
  person: null,
  name: document.querySelector(".right .top .name"),
};

friends.all.forEach((f) => {
  f.addEventListener("click", () => {
    if (!f.classList.contains("active")) {
      setActiveChat(f);
    }
  });
});

function setActiveChat(f) {
  const activePerson = document.querySelector(".left .person.active");
  if (activePerson) {
    activePerson.classList.remove("active");
  }

  const activeChat = document.querySelector(".right .chat.active-chat");
  if (activeChat) {
    activeChat.classList.remove("active-chat");
  }

  f.classList.add("active");
  chat.current = chat.container.querySelector(
    `.chat[data-chat='${f.getAttribute("data-chat")}']`
  );
  chat.current.classList.add("active-chat");
  friends.name = f.querySelector(".name").innerText;
  chat.name.innerHTML = friends.name;
}

document.querySelectorAll(".left .person").forEach((f) => {
  f.addEventListener("click", () => {
    if (!f.classList.contains("active")) {
      setActiveChat(f);
    }
  });
});
function addMessage(message, avatarSrc) {
  const chatContent = document.querySelector(".chat_content");

  // Tạo phần tử hình ảnh avatar
  const avatarImg = document.createElement("img");
  avatarImg.src = avatarSrc;
  avatarImg.classList.add("img-fluid", "rounded-top", "avatar");
  chatContent.appendChild(avatarImg);

  // Tạo phần tử bubble tin nhắn
  const bubble = document.createElement("div");
  bubble.classList.add("bubble", "you");
  bubble.innerText = message;
  chatContent.appendChild(bubble);
}

// Sử dụng addMessage để thêm tin nhắn mới và avatar tương ứng
// addMessage("Hello,", "assets/img/mascot_fox.png");
// addMessage("it's me.", "assets/img/mascot_fox.png");
// addMessage("I was wondering...", "assets/img/mascot_fox.png");
function openNav() {
  document.getElementById("mySidenav").style.display = "block";
}

function closeNav() {
  document.getElementById("mySidenav").style.display = "none";
}

//=== Change Tag Search ===//
document.addEventListener("DOMContentLoaded", function () {
    const navItems = document.querySelectorAll(".subnavigate ul li");
    const contentItems = document.querySelectorAll(".content_table");

    navItems.forEach((item, index) => {
        item.addEventListener("click", function () {
            navItems.forEach((navItem) => navItem.classList.remove("active"));
            contentItems.forEach((contentItem) =>
                contentItem.classList.remove("active")
            );
            item.classList.add("active");
            if (contentItems) {
                contentItems[index].classList.add("active");
            }
        });
    });
});
