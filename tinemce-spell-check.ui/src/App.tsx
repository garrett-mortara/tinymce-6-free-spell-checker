import { Editor } from "@tinymce/tinymce-react";

function App() {
  return (
    <Editor tinymceScriptSrc={process.env.PUBLIC_URL + '/tinymce/tinymce.min.js'} />
  );
}

export default App;
