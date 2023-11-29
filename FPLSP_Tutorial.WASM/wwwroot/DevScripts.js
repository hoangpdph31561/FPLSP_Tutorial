function CreateRTEInstance() { 
    var config = {}
    config.editorResizeMode = "height";
    window.editor1 = new RichTextEditor("#inp_editor1", config);
}

function SetRTEValue(value) {
    editor1.setHTMLCode(value);
}

function GetRTEValue(value) { 
    return editor1.getHTMLCode();
}

function ReadClipboard() {
    navigator.clipboard.readText()
        .then(function (clipText) {
            alert("Clipboard content: " + clipText);
        })
        .catch(function (error) {
            console.error("Error reading clipboard:", error);
        });
}