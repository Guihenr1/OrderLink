variable "resource_group_name_prefix" {
  description = "The prefix to use for the resource group name"
  type        = string
  default     = "infra-ol"
}

variable "resource_group_location" {
  description = "The location to create the resource group in"
  type        = string
  default     = "Central US"
}
