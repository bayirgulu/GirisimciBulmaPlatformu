/**
 * WEBSITE: https://themefisher.com
 * TWITTER: https://twitter.com/themefisher
 * FACEBOOK: https://facebook.com/themefisher
 * GITHUB: https://github.com/themefisher/
 */

// import Swiper from "../plugins/swiper/swiper-bundle.js";
// import Shuffle from "../plugins/shufflejs/shuffle";

(function () {
  "use strict";

  // Preloader js
  // window.addEventListener("load", (e) => {
  //   document.querySelector(".preloader").style.display = "none";
  // });

  //sticky header
  /*const header = document.querySelector(".header");
  window.addEventListener("scroll", () => {
    const scrollY = window.scrollY;
    if (scrollY > 0) {
      header.classList.add("header-sticky");
    } else {
      header.classList.remove("header-sticky");
    }
  });*/

    window.loadGallery = function () {
        let galleries = [...document.querySelectorAll('.gallery')];
        console.log(galleries)
        for (var gallery of galleries) {
            let swiper = new Swiper(gallery.parentElement, {
                // other parameters
                navigation: {
                    nextEl: '.swiper-button-next',
                    prevEl: '.swiper-button-prev',
                },

                // Init lightGallery ince swiper is initilized
                on: {
                    init: function () {
                        const lg = lightGallery(gallery, {
                            speed: 300,
                            download: false,
                            height: "auto",
                        });

                        // Before closing lightGallery, make sure swiper slide
                        // is aligned with the lightGallery active slide
                        gallery.addEventListener('lgBeforeClose', () => {
                            swiper.slideTo(lg.index, 0)
                        });
                    },
                }
            });
        }
    }

  // for tab component
  // Get all the tab groups on the page
  const tabGroups = document.querySelectorAll("[data-tab-group]");
  // Loop through each tab group
  tabGroups.forEach((tabGroup) => {
    // Get the tabs nav and content for this tab group
    const tabsNav = tabGroup.querySelector("[data-tab-nav]");
    const tabsNavItem = tabsNav.querySelectorAll("[data-tab]");

    // Get the active tab index from local storage, or default to 0 if not set
    const activeTabName =
      localStorage.getItem(`activeTabName-${tabGroup.dataset.tabGroup}`) ||
      tabsNavItem[0].getAttribute("data-tab");

    // Set the active tab
    setActiveTab(tabGroup, activeTabName);

    // Add a click event listener to each tab nav item
    tabsNavItem.forEach((tabNavItem) => {
      tabNavItem.addEventListener("click", (e) => {
        e.preventDefault();
        // Get the index of the clicked tab nav item
        const tabName = tabNavItem.dataset.tab;
        setActiveTab(tabGroup, tabName);

        // Save the active tab index to local storage
        localStorage.setItem(
          `activeTabName-${tabGroup.dataset.tabGroup}`,
          tabName
        );
      });
    });
  });

  // Function to set the active tab for a given tab group
  function setActiveTab(tabGroup, tabName) {
    // Get the tabs nav and content for this tab group
    const tabsNav = tabGroup.querySelector("[data-tab-nav]");
    const tabsContent = tabGroup.querySelector("[data-tab-content]");

    // Remove the active class from all tab nav items and content panes
    tabsNav.querySelectorAll("[data-tab]").forEach((tabNavItem) => {
      tabNavItem.classList.remove("active");
    });
    tabsContent.querySelectorAll("[data-tab-panel]").forEach((tabPane) => {
      tabPane.classList.remove("active");
    });

    // Add the active class to the selected tab nav item and content pane
    const selectedTabNavItem = tabsNav.querySelector(`[data-tab="${tabName}"]`);
    selectedTabNavItem.classList.add("active");
    const selectedTabPane = tabsContent.querySelector(
      `[data-tab-panel="${tabName}"]`
    );
    selectedTabPane.classList.add("active");
  }

  //counter
  function counter(el, duration) {
    const endValue = Number(el.innerText.replace(/\D/gi, ""));
    const text = el.innerText.replace(/\W|\d/gi, "");
    const timeStep = Math.round(duration / endValue);
    let current = 0;
    const timer = setInterval(() => {
      if (current > endValue) {
        current = endValue;
      } else {
        current += 1;
      }
      el.innerText = current + text;
      if (current === endValue) {
        clearInterval(timer);
      }
    }, timeStep);
  }

  document.querySelectorAll(".counter .count").forEach((count) => {
    counter(count, 500);
  });

  //play youtube-video
  const videoPlayBtn = document.querySelector(".video-play-btn");
  if (videoPlayBtn) {
    videoPlayBtn.addEventListener("click", function () {
      const videoPlayer = this.closest(".video").querySelector(".video-player");
      videoPlayer.classList.remove("hidden");
    });
  }

  // Accordion component
  const accordion = document.querySelectorAll("[data-accordion]");
  accordion.forEach((header) => {
    header.addEventListener("click", () => {
      const accordionItem = header.parentElement;
      accordionItem.classList.toggle("active");
    });
  });

    //shuffle
    //window.loadFilters = function () {
    //    const Shuffle = window.Shuffle;
    //    const tabItems = document.querySelector(".integration-tab-items");
    //    if (tabItems) {
    //        const myShuffle = new Shuffle(tabItems, {
    //            itemSelector: ".integration-tab-item",
    //            sizer: ".integration-tab-item",
    //            buffer: 1,
    //        });
    //        const tabLinks = document.querySelectorAll(".integration-tab .filter-btn");
    //        tabLinks.forEach((tabItem) => {
    //            tabItem.addEventListener("click", function (e) {
    //                e.preventDefault();
    //                let filter;
    //                const group = tabItem.getAttribute("data-group");
    //                filter = group;
    //                if (filter === "all") {
    //                    filter = Shuffle.ALL_ITEMS;
    //                }
    //                tabLinks.forEach((link) => link.classList.remove("filter-btn-active"));
    //                this.classList.add("filter-btn-active");
    //                myShuffle.filter(filter);
    //            });
    //        });
    //        [...tabLinks].filter(links => links.classList.contains("filter-btn-active"))[0]?.click();
    //    }
    //}

    window.loadUploadInput = function () {

        window.filesList = [];

        var dragDropArea = document.getElementById('drag-drop-area');

        dragDropArea.addEventListener('dragover', function (event) {
            event.preventDefault();
            dragDropArea.classList.add('drag-over');
        });

        dragDropArea.addEventListener('dragleave', function (event) {
            dragDropArea.classList.remove('drag-over');
        });

        dragDropArea.addEventListener('drop', function (event) {
            event.preventDefault();
            dragDropArea.classList.remove('drag-over');
            //window.filesList = window.filesList.concat(Array.from(event.dataTransfer.files));
            //updatePreview();
        });

        //function updatePreview() {
        //    var previewContainer = document.getElementById('file-preview');
        //    previewContainer.innerHTML = '';

        //    filesList.forEach((file, index) => {
        //        var reader = new FileReader();
        //        reader.onload = function (e) {
        //            var imgContainer = document.createElement('div');
        //            imgContainer.classList.add('image-container');

        //            var img = document.createElement('img');
        //            img.src = e.target.result;
        //            imgContainer.appendChild(img);

        //            var deleteBtn = document.createElement('button');
        //            deleteBtn.classList.add('delete-btn');
        //            deleteBtn.innerText = 'X';
        //            deleteBtn.onclick = function () {
        //                filesList.splice(index, 1);
        //                updatePreview();
        //            };
        //            imgContainer.appendChild(deleteBtn);

        //            previewContainer.appendChild(imgContainer);
        //        };
        //        console.log(file);
        //        reader.readAsDataURL(file);
        //    });
        //}
    }

  // Set active navigation
  function setNavigation() {
      var path = window.location.pathname;
      path = path.replace(/\/$/, "");
      path = decodeURIComponent(path);

      const navigation = document.querySelectorAll("#nav-menu > li");

      navigation.forEach((list) => {
        var link = list.querySelector('a'),
          href = link.getAttribute('href');
          link.classList.remove("active");

          if ( path.split('/')[1] === href) {
            link.classList.add("active");
          }
      });
  }
  setNavigation();


})();
