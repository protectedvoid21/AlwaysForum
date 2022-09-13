function showWarning(message) {
    if (confirm(message) === false) {
        event.preventDefault()
        return
    }
}