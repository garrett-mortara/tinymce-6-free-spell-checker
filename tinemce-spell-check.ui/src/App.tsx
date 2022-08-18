import { Editor } from "@tinymce/tinymce-react";

function App() {
  return (
    <Editor
      tinymceScriptSrc={process.env.PUBLIC_URL + "/tinymce/tinymce.js"}
      init={{
        menubar: false,
        plugins: [
          "spellchecker",
        ],
        toolbar:
          "spellchecker",
      }}
    />
  );
}

export default App;
