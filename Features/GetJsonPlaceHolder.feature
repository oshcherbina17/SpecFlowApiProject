Feature: Get JsonPlaceHolder
  This feature tests the JSONPlaceholder API for retrieving a specific post.

  Scenario: Successfully retrieve a post
    Given API endpoint "https://jsonplaceholder.typicode.com/posts/1"
    When a GET request is sent
    Then response status should be 200
    And response should contain "id" with value "1"
    And response should contain non-empty "title"
    And response should contain non-empty "body"

