function convertToHTML(text) {
    let parser = new DOMParser()
    let doc = parser.parseFromString(text)

    return doc.body
}