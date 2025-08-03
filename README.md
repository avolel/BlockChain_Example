# BlockChain Example - A Simple Blockchain Simulation in .NET Core

This project provides a simplified demonstration of how a blockchain works. It's designed for educational purposes to illustrate core concepts like blocks, transactions, mining, and smart contracts. It is implemented using C# and .NET Core.

## Project Overview

The code simulates a basic blockchain with the following key features:

- **Block Creation:** Blocks are created containing transaction data, timestamps, and cryptographic hashes.
- **Transaction Management:** Transactions represent transfers of value between wallets.
- **Mining:** A simplified proof-of-work mining algorithm is implemented to secure blocks.
- **Smart Contracts:** A basic smart contract (AirDropContract) demonstrates how contracts can be executed on the blockchain.
- **Wallet Management:** Allows for creation and management of user wallets with balances.

## Core Classes & Components

Here's a breakdown of each class and its purpose:

### 1. `Transaction` (Models/Transaction.cs)

- Represents a transaction within the blockchain.
- **Properties:**
  - `Sender`: The address sending the funds.
  - `Receiver`: The address receiving the funds.
  - `Amount`: The amount of currency being transferred.
- **Methods:**
  - `ToString()`: Provides a string representation of the transaction (e.g., "Sender -> Receiver: Amount").

### 2. `Block` (Models/Block.cs)

- Represents a block in the blockchain.
- **Properties:**
  - `Transactions`: A list of transactions included in this block.
  - `MerkleRoot`: The Merkle root hash, representing all transactions in the block.
  - `Index`: The position of the block in the chain (its number).
  - `TimeStamp`: When the block was created.
  - `PreviousHash`: The hash of the previous block in the chain, linking blocks together.
  - `Hash`: The unique identifier for this block, calculated from its contents.
  - `Nonce`: A value used in the mining process to find a valid hash.
- **Methods:**
  - `CalculateHash()`: Calculates the SHA256 hash of the block's data (index, timestamp, Merkle root, previous hash, nonce). This is how the block's integrity is ensured.
  - `MineBlock(int difficulty)`: Implements a simplified proof-of-work mining algorithm. It iteratively increments the `Nonce` value until the calculated `Hash` starts with a specified number of leading zeros (determined by the `difficulty`).

### 3. `MerkleTree` (Models/MerkleTree.cs)

- Provides functionality for calculating Merkle roots.
- **Methods:**
  - `ComputeMerkleRoot(List<Transaction> transactions)`: Calculates the Merkle root hash from a list of transactions. This ensures that any tampering with a transaction within a block will change the Merkle root, invalidating the block.

### 4. `BlockChain` (Models/BlockChain.cs)

- Represents the blockchain itself.
- **Properties:**
  - `Chain`: A list of blocks forming the chain.
  - `Difficulty`: The difficulty level for mining (number of leading zeros required in the block hash).
  - `pending_transactions`: A list of transactions waiting to be included in a new block.
- **Methods:**
  - `GetLatestBlock()`: Returns the most recently added block to the chain.
  - `AddBlock(Block new_block)`: Adds a new block to the blockchain, setting its `PreviousHash`.
  - `AddTransaction(Transaction transaction)`: Adds a pending transaction to the `pending_transactions` list.
  - `MinePendingTransactions(string minerAddress)`: Creates a new block from the `pending_transactions`, rewards the specified miner with a small amount of currency, mines the block using proof-of-work, and adds it to the chain.
  - `IsChainValid()`: Verifies the integrity of the blockchain by checking that each block's hash is valid and that the `PreviousHash` values are correct.
  - `GetBalance(string address)`: Calculates the balance of a given wallet address by iterating through all transactions in the chain.

### 5. `Wallet` (Models/Wallet.cs)

- Represents a user's wallet within the blockchain system.
- **Properties:**
  - `Address`: The unique identifier for the wallet.
- **Methods:**
  - `CreateTransaction(string receiver, decimal amount)`: Creates a new transaction from this wallet to a specified receiver with a given amount.

### 6. `AirDropContract` (Models/SmartContracts/AirDropContract.cs)

- Represents a simple smart contract for distributing currency to a specific address.
- **Properties:**
  - `Receiver`: The address receiving the airdropped funds.
  - `Amount`: The amount of currency being distributed.
- **Methods:**
  - `Execute(BlockChain chain)`: Creates a transaction that transfers the specified `Amount` to the `Receiver` and adds it to the blockchain's pending transactions.

### 7. `ISmartContract` (Models/SmartContracts/ISmartContract.cs)

- Defines an interface for smart contracts, ensuring they can be executed on the blockchain.
- **Methods:**
  - `Execute(BlockChain chain)`: Defines the method that a smart contract must implement to execute its logic within the blockchain context.

## Project Structure

The project is organized into several namespaces:

- `BlockChain_Example.Models`: Contains core data structures like `Block`, `Transaction`, `BlockChain`, and `Wallet`.
- `BlockChain_Example.Models.SmartContracts`: Defines the interface for smart contracts (`ISmartContract`) and implements a sample contract (`AirDropContract`).

## Usage (Console Application)

The project is a console application that provides a command-line interface (CLI) to interact with the blockchain:

1. **Create a Wallet:** `dotnet run` then enter '1' at the prompt.
2. **View Balance:** Enter '2', and provide the wallet name.
3. **Create Transaction:** Enter '3', providing sender, receiver, and amount. Ensure the sender has sufficient balance.
4. **Mine Block:** Enter '4', specifying a miner address. This adds new transactions to the blockchain.
5. **View Chain:** Enter '5' to display the contents of the blockchain.
6. **Air Drop:** Enter '6', providing a receiver and amount for an airdrop. This creates a pending transaction that needs to be mined.
7. **Exit:** Enter '7' to terminate the application.

## Technologies Used

- **C#:** The programming language used to implement the blockchain logic.
- **.NET Core:** The runtime environment for executing the application.
- **SHA256:** A cryptographic hash function used for securing blocks and transactions.

## Limitations & Future Enhancements

- **Simplified Consensus:** This implementation uses a very basic proof-of-work mechanism. Real blockchains employ more sophisticated consensus algorithms.
- **No Network Communication:** The blockchain is currently single-node, meaning there's no peer-to-peer network for synchronization.
- **Basic Smart Contracts:** The smart contract functionality is limited to the Airdrop example. More complex contracts could be implemented.
- **Security Considerations:** This is a simplified demonstration and does not include robust security measures found in production blockchains.
