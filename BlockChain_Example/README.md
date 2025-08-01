# BlockChain Example - .NET Core Implementation

This project provides a simplified implementation of a blockchain using .NET Core. It demonstrates core concepts like blocks, transactions, Merkle trees, mining, and basic smart contract functionality. This is intended as an educational example rather than a production-ready blockchain solution.

## Project Structure & Overview

The project consists of several classes representing key components:

*   **`Block.cs`**: Defines the `Block` class, which represents a block in the blockchain. Each block contains transactions, a Merkle root (representing the hash of all transactions), an index, timestamp, previous hash, its own calculated hash, and a nonce used for mining.
    *   The `CalculateHash()` method computes the SHA256 hash of the block's data.
    *   The `MineBlock(int difficulty)` method performs proof-of-work by incrementing the nonce until the block's hash starts with a specified number of leading zeros (determined by the `difficulty`).

*   **`Transaction.cs`**: Defines the `Transaction` class, representing a transfer of value between two parties.  It includes sender, receiver, and amount fields. The `ToString()` method provides a human-readable representation of the transaction.

*   **`MerkleTree.cs`**: Implements the `MerkleTree` static class for calculating the Merkle root of a list of transactions.
    *   The `ComputeMerkleRoot(List<Transaction> transactions)` method recursively hashes pairs of transaction hashes until a single root hash remains. This ensures data integrity within each block.  If an odd number of transactions exists, the last transaction is hashed with itself.
    *   The `Hash(string input)` method calculates the SHA256 hash of a given string.

*   **`BlockChain.cs`**: Defines the `BlockChain` class, which manages the chain of blocks.
    *   It maintains a list of `Block` objects (`Chain`).
    *   The `Difficulty` property controls how difficult it is to mine new blocks (more leading zeros required).
    *   `pending_transactions` stores transactions that are not yet included in a block.
    *   `GetLatestBlock()` returns the most recently added block.
    *   `AddBlock(Block new_block)` adds a new block to the chain, setting its `PreviousHash`.
    *   `AddTransaction(Transaction transaction)` adds a transaction to the pending transactions list.
    *   `MinePendingTransactions(string minerAddress)` creates a new block containing the pending transactions (including a reward for the miner), mines it, and adds it to the chain.
    *   `IsChainValid()` checks if the blockchain is valid by verifying that each block's hash matches its calculated value and that the `PreviousHash` of each block correctly references the previous block’s hash.

*   **`AirDropContract.cs`**:  Implements a simple smart contract (`AirDropContract`) that automatically distributes an amount to a specified receiver.
    *   It implements the `ISmartContract` interface.
    *   The `Execute(BlockChain chain)` method adds a transaction to the blockchain's pending transactions, effectively triggering the airdrop.

*   **`ISmartContract.cs`**: Defines the `ISmartContract` interface, which allows for different smart contract types to be implemented and executed within the blockchain system.

## Key Concepts Demonstrated

*   **Blockchain Structure:** The code demonstrates how blocks are linked together chronologically using previous hashes.
*   **Transactions:**  Represents transfers of value between participants.
*   **Merkle Trees:** Used for efficient verification of transaction data integrity within a block.
*   **Hashing (SHA256):** Provides cryptographic security and ensures data immutability.
*   **Proof-of-Work (Mining):** The `MineBlock` method simulates the mining process, where miners compete to find a nonce that results in a hash meeting the difficulty requirement.  This secures the blockchain by making it computationally expensive to alter past blocks.
*   **Smart Contracts:** A basic example of how contracts can be integrated into the blockchain to automate actions (in this case, an airdrop).

## How to Run

1.  **Prerequisites:** .NET Core SDK installed.
2.  **Build:** Navigate to the project directory in your terminal and run `dotnet build`.
3.  **Run:** After building, you can execute the code (you'll need to add a main program or console application that utilizes these classes). A basic example would involve creating a `BlockChain` instance, adding transactions, mining blocks, and validating the chain.

## Potential Enhancements & Future Development

*   **More Sophisticated Smart Contracts:** Implement more complex smart contract logic.
*   **Peer-to-Peer Networking:**  Enable communication between multiple blockchain nodes.
*   **Transaction Fees:** Introduce transaction fees to incentivize miners.
*   **Consensus Mechanisms:** Explore different consensus algorithms beyond proof-of-work (e.g., Proof of Stake).
*   **Wallet Integration:** Add functionality for managing user wallets and keys.
*   **GUI/CLI Interface:** Create a more user-friendly interface for interacting with the blockchain.