var searchHandler;

$(document).ready(function () {
    initializeSearch();
});

function initializeSearch() {
    // Trigger on typing
    $('#searchInput').on('keyup', function () {
        applySearch();
    });

    // Trigger on radio change
    $('input[name="searchFilter"]').on('change', function () {
        applySearch();
    });
}
function applySearch() {

    let searchText = $('#searchInput').val().toLowerCase();
    let filter = $('input[name="searchFilter"]:checked').val();

    let visibleCount = 0;

    $('.book-card').each(function () {

        let title = $(this).data('title');
        let author = $(this).data('author');

        let match = false;

        if (filter === "all") {
            match = title.includes(searchText) || author.includes(searchText);
        }
        else if (filter === "title") {
            match = title.includes(searchText);
        }
        else if (filter === "author") {
            match = author.includes(searchText);
        }

        if (match) {
            $(this).show();
            visibleCount++;
        } else {
            $(this).hide();
        }

    });

    showSearchMessage(searchText, visibleCount);
}
function showSearchMessage(searchText, count) {

    if (searchText === "") return;

    if (count === 0) {
        toastr.warning("No books found!");
    } else {
        // optional success message
        console.log(count + " results found");
    }
}
function showSearchMessage(searchText, count) {

    if (searchText === "") return;

    if (count === 0) {
        swal({
            title: "No Results",
            text: "No books found for your search",
            icon: "warning",
            button: "OK"
        });
    }
}