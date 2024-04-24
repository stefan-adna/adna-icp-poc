import Map "mo:base/HashMap";
import Text "mo:base/Text";
import List "mo:base/List";
import Array "mo:base/Array";

actor {

  type RootHash = Text;
  type Time = Int;

  type Transaction = {
    TransactionId : Text;
    ClientIdHash: Text;
    CreateDateTime: Time;
  };

  let rootHashStore = Map.HashMap<RootHash, [Transaction]>(0, Text.equal, Text.hash);

  public func storeRootHash(rootHash : RootHash, transaction : Transaction): async () {
    let existingList : ?[Transaction] = rootHashStore.get(rootHash);
    switch(existingList) {
      case (?current) {
        //root hash exists -> append new hash
        let updatedList = Array.append<Transaction>(current, [transaction]);
        rootHashStore.put(rootHash, updatedList);
      };
      case null {
        //root hash does not exist -> create new list for it
        let newList = [transaction];
        rootHashStore.put(rootHash, newList);
      }
    }
  };

  public query func getRootHash(rootHash : RootHash) : async ?[Transaction] {
    rootHashStore.get(rootHash)
  };
};