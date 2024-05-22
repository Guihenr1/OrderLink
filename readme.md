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

__Why Onion architecture?__ 

I chose to use Onion Architecture because It is centered around the modularity, testability and the principle of dependency inversion.
I separated in four layers:

* **Presentation:** Handle the interaction with the user with the input/output data.

* **Application:** Handle the operations of high level involving more than one agregate or entity domain and is responsible for handle the data came from the Kitchen throught the queue.

* **Domain:** The core bunissess of the application. Represent the entities and interactions that handle these attributes.

* **Infrastructure:** Provide access to the database, external services and frameworks.

</details>

### Coding Practices

### Testing

### Deployment 

### Infrastructure

### Tools
