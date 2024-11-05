//=== Search Popover ===//
let timeOut = null;
let divSearchResult = document.getElementById('divSearchResult');
let inputSearch = document.getElementById('inputSearch');
let searchForm = document.getElementById('searchForm');
let currentPopover = null;

// Function to create popover content dynamically with links to the full search page
function createPopoverContent(stories) {
    let content = '<ul class="list-group">';
    stories.forEach(story => {
        // Create each search result as a link that navigates to the search page
        content += `<li class="list-group-item" data-query="${story.storyTitle}">
                                <a href="/Home/FullSearch?query=${encodeURIComponent(story.storyTitle)}" class="text-decoration-none text-dark">
                                    ${story.storyTitle}
                                </a>
                            </li>`;
    });
    content += '</ul>';
    return content;
}

function createPopoverContentV2(stories) {
    let content = '<ul class="list-group">';
    stories.forEach(story => {
        // Create each search result as a link that navigates to the search page
        content += `<li class="list-group-item" data-query="${story.storyTitle}">
                                <span onclick="filterStory('${story.storyTitle}')" class="text-decoration-none text-dark">
                                    ${story.storyTitle}
                                </span>
                            </li>`;
    });
    content += '</ul>';
    return content;
}

// Function to show popover
function showPopover(target, content) {
    let popover = document.createElement('div');
    popover.classList.add('popover', 'bs-popover-bottom');
    popover.innerHTML = `
                <div class="popover-arrow"></div>
                <div class="popover-body">
                    ${content}
                </div>
            `;
    document.body.appendChild(popover);

    // Position the popover
    let rect = target.getBoundingClientRect();
    popover.style.position = 'absolute';
    popover.style.top = rect.bottom + window.scrollY + 'px';
    popover.style.left = rect.left + window.scrollX + 'px';

    return popover;
}

// Function to hide popover
function hidePopover(popover) {
    if (popover) {
        popover.remove();
    }
}

// AJAX function to get search results and render PartialView in popover
function getSearchResult(textSearch) {
    clearTimeout(timeOut);

    let xhr = new XMLHttpRequest();
    xhr.open('GET', `/Home/Search?textSearch=${encodeURIComponent(textSearch)}`, true);
    xhr.onload = function () {
        if (xhr.status === 200) {
            if (divSearchResult) {
                divSearchResult.innerHTML = '';  // Clear if needed
            }

            // If there's an existing popover, hide it
            if (currentPopover) {
                hidePopover(currentPopover);
            }

            // Show new popover with PartialView content
            let stories = JSON.parse(xhr.responseText); // Assuming the response is JSON
            let popoverContent = createPopoverContent(stories);
            //let popoverContent = createPopoverContentV2(stories);
            currentPopover = showPopover(inputSearch, popoverContent);

            // Add event listener to each search result
            document.querySelectorAll('.search-item').forEach(item => {
                item.addEventListener('click', function () {
                    let query = this.getAttribute('data-query');
                    window.location.href = `/Home/FullSearch?query=${encodeURIComponent(query)}`;
                });
            });
        }
    };
    xhr.onerror = function () {
        console.error('Error during AJAX request');
    };
    xhr.send();
}

// Function to handle form submit or enter key
function handleSearch() {
    let textSearch = inputSearch.value;
    if (textSearch.length > 0) {
        getSearchResult(textSearch);
        //filterStory();
    } else {
        hidePopover(currentPopover);
        currentPopover = null;
    }
}

if (inputSearch) {
    // Delay search execution
    timeOut = setTimeout(function () {
        inputSearch.addEventListener('keyup', function (event) {
            if (event.key === 'Enter') { // Handle when user presses Enter
                //event.preventDefault();
                //handleSearch();
                filterStory();
            } else {
                let textSearch = inputSearch.value;
                if (textSearch.length > 0) {
                    getSearchResult(textSearch);
                } else {
                    hidePopover(currentPopover);
                    currentPopover = null;
                }
            }
        });
    }, 1500);
}

// Event listener for form submit (when user clicks the button)
if (searchForm) {
    searchForm.addEventListener('submit', function (event) {
        event.preventDefault();  // Prevent form from submitting
        handleSearch();  // Handle search with popover logic
    });
}

// Event listener to hide popover when clicking outside
document.addEventListener('click', function (event) {
    if (currentPopover && !inputSearch.contains(event.target) && !currentPopover.contains(event.target)) {
        hidePopover(currentPopover);
        currentPopover = null;
    }
});

//==== Filter in Search Page ====//
const tags = [];
document.querySelectorAll('.tag_filter').forEach(tag => {
    tag.addEventListener('click', function (e) {
        e.preventDefault();
        const value = this.getAttribute('data-value');
        if (tags.includes(value)) {
            tags.splice(tags.indexOf(value), 1); // Remove tag if it's already selected
            this.classList.remove('selected');
        } else {
            tags.push(value); // Add tag to array
            this.classList.add('selected');
        }
        document.getElementById('tagsInput').value = tags.join(','); // Update hidden input value
    });
});

function filterStory(button, defaultQuery = null) {
    let resultChapterCount = 0;

    // Lấy chapter count từ checkbox
    var ulChapterCount = document.getElementById("chapterCount");
    if (ulChapterCount) {
        let arrayChapterCount = ulChapterCount.querySelectorAll('input[name="lengthFilter"]');
        arrayChapterCount.forEach(chapterCount => {
            if (chapterCount.checked) {
                resultChapterCount = chapterCount.value;
            }
        });
    }

    let inputQuery = document.getElementById("inputSearch");

    if (!defaultQuery) {
        defaultQuery = inputQuery.value;
    }

    console.log('Query:', defaultQuery);

    $.ajax({
        type: "POST",
        url: "/Home/SearchResult",
        data: {
            query: defaultQuery,
            chapterCountIndex: resultChapterCount
        },
        success: function (data) {
            let divStoryResult = document.getElementById("resultStory");
            if (divStoryResult) {
                divStoryResult.innerHTML = data;
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}