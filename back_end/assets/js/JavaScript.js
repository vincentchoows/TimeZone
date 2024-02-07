var sections = document.querySelectorAll('.nav-link');

sections.forEach(function (section) {
    section.addEventListener('click', function () {
        // remove the "active" class from all sections
        sections.forEach(function (s) {
            s.classList.remove('active');
        });

        // add the "active" class to the clicked section
        section.classList.add('active');
    });
});