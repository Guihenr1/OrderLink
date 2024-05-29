# Order Link

Order Link is a project consisting of two modules: Kitchen and Order. They communicate with each other via a RabbitMQ queue. It is a software solution for restaurants to register orders. 
The functionality allows users to create dishes, and other users can register orders for these dishes. 

## How to Execute This Application

## How to Create the Infrastructure in Azure with Terraform

## Rationale and Best Practices

This project follows best practices to deliver a scalable and stable solution that can be used as a guide to create other projects or as a study resource.

### Architecture

For architectural decisions, I chose to separate the Kitchen and Order modules to adhere to the separation of concerns principle. Additionally, I made other decisions to maintain clean code.

<details><summary><b>Show architecture decisions</b></summary>

__Onion architecture__ 

I chose to use Onion Architecture because It is centered around the modularity, testability and the principle of dependency inversion.
I separated in four layers:

* **Presentation:** Handle the interaction with the user with the input/output data.

* **Application:** Handle the operations of high level involving more than one agregate or entity domain and is responsible for handle the data came from the Kitchen throught the queue.

* **Domain:** The core bunissess of the application. Represent the entities and interactions that handle these attributes.

* **Infrastructure:** Provide access to the database, external services and frameworks.


__Repository Pattern__

I created a class that is designed for an entity that inherits from EntityBase, generating the most commonly used database operations. This class provides an interface for operations while encapsulating the complexities of data access.
The EntityBase is an abstract class that contains the common attributes of all entities and is used in Repository Pattern.
You can find more details about REpository Pattern in my [article](https://medium.com/@guilherme.pomp/repository-design-pattern-in-net-core-1b050679c3a2).


</details>

### Coding Practices

Maintaining good practices in the code is crucial for keeping it scalable. You can see why I chose to use some of them in this section.

<details><summary><b>Show code practices decisions</b></summary>

__IEnumerable__

I used to return a list from database because this interface provices a high level of abstraction It makes the code more flexible because you're not committing to a specific type of collection. Additionality, It's a read-only list that ensure the collection is not accidentally modified.

__Notification Pattern__

This provides a way to handle and communicate different types of messages, such as errors, information, and warnings, in a consistent and flexible manner within different parts of the code, thus avoiding indiscriminate use of exceptions.


</details>

### Testing

For testing, I am using the XUnit library.

<details><summary><b>Show testing decisions</b></summary>

__Testing and Code Coverage__

I am testing the core logic with XUnit, aiming to achieve a high level of code coverage. I focus on testing the main logic, which comprises the critical points in the software.


</details>

### Deployment 

For local deployment, I am using Docker Compose.

<details><summary><b>Show deployment decisions</b></summary>

__Docker and Docker Compose__

You can easily deploy using Docker Compose, which is configured to build the entire project environment, facilitating the deployment of SQL Server, RabbitMQ, and the project itself.


</details>

### Infrastructure

As Infrastructure as a Code, I am using Terraform.

<details><summary><b>Show infrastructure decisions</b></summary>

__Terraform__

With one command, you can create the infrastructure to deploy in Azure. This allows for versioning and automates the infrastructure.


</details>

### Tools

I utilized some tools to enhance code quality.

<details><summary><b>Show tools decisions</b></summary>

__SonarQube__

Performs automatic reviews of code to detect bugs, code smells, and security vulnerabilities.


</details>
