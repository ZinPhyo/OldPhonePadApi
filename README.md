# 📱 OldPhonePad API

OldPhonePad API converts numeric keypad input (like old mobile phones) into corresponding text.  
It supports **backspace (`*`)**, **send (`#`)**, and multi-digit/word sequences — accurately simulating old mobile text entry logic.

---

## ✨ Features

- 🔢 Convert numeric keypad sequences into letters  
- ⌫ Handle backspace using `*`  
- 🚀 Send message with `#`  
- 🧠 Supports multiple words separated by spaces  
- ✅ Fully tested with unit tests (xUnit + Moq)

---

## 🧭 API Overview

### **Endpoint**
### **Request Body**

```json
{
  "input": "4433555 555666#"
}
{
  "output": "HELLO"
}



