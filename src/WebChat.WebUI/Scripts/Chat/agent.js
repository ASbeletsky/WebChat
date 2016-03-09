function animate_menu () {
    $('.circle')
        .hover(function () {
            $(this).animate({ border: '10px solid #f79215' }, 300)
        },
        function () {
            $(this).animate({ border: '10px solid #f79215' }, 300)
        })
};
