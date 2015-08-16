$("#loginPrompt").popover({
    html: true,
    placement: "bottom",
    content: function () {
        return $('#popoverLogin').html();
    },
    title: function () {
        return "<h5>Credentials</h5>"
    }
});