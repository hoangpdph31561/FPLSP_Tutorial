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
