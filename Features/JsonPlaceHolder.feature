Feature: JsonPlaceHolder API Testing
  This feature tests the JSONPlaceholder API for creating a new post.

  Scenario: Successfully create a new post
    Given the API endpoint is "https://jsonplaceholder.typicode.com/posts"
    And the request body contains "body" with value "this body"
    And the request body contains "title" with value "this title"
    When a POST request is sent
    Then the response status code should be 201
    And the response should contain the "body" with value "this body"
    And the response should contain the "title" with value "this title"
