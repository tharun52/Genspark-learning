# FaqService.cs

## Overview

`FaqService` is a service class in the BankApp project that provides answers to user questions by leveraging both a local FAQ repository and the Groq LLM API. It is designed to act as a smart FAQ bot for banking-related queries.

---

## Key Features

- **FAQ Filtering:** Selects up to 5 relevant FAQs based on user input to keep API requests concise.
- **LLM Integration:** Sends a prompt (including relevant FAQs and user question) to the Groq API for a formal, context-aware response.
- **API Key Management:** Reads the Groq API key from environment variables or configuration.
- **Error Handling:** Handles HTTP and JSON parsing errors gracefully.

---

## Usage

### Constructor

```csharp
public FaqService(
    IFaqRepository faqRepository,
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration)
```

- `faqRepository`: Provides access to stored FAQs.
- `httpClientFactory`: Used to create an authenticated HTTP client.
- `configuration`: Used to retrieve the `GROQ_API_KEY`.

### Main Method

```csharp
public async Task<string> GetAnswerAsync(string userInput)
```
- Filters relevant FAQs.
- Builds a prompt and sends it to the Groq API.
- Returns the LLM's answer or an error message.

---

## Environment Variables

- **GROQ_API_KEY**: Must be set in your environment or `.env` file for the service to authenticate with the Groq API.

---

## Example .env

```
GROQ_API_KEY=your_actual_api_key_here
```

---

## Error Handling

- Returns a user-friendly message if the API response cannot be parsed.
- Throws an exception if the API key is missing.

---

## Notes

- If you receive a `413 Payload Too Large` error, ensure that only a small, relevant subset of FAQs is included in each request.
- The service expects the Groq API to be available and the API key to be valid.

---