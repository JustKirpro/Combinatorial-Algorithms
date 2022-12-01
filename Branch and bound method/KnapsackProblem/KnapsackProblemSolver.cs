using System.Collections.Generic;
using System.Linq;

namespace KnapsackProblem;

public class KnapsackProblemSolver
{
    private List<Item> _items;
    private int _itemsNumber;
    private int _maxWeight;
    
    public List<int> Solve(List<Item> items, int maxWeight)
    {
        _items = items;
        _items = items.OrderBy(item=>-item.UnitValue).ToList();
        _items.Add(new Item(1, 0, _itemsNumber + 1));
        _itemsNumber = items.Count;
        _maxWeight = maxWeight;
        
        var priorityQueue = new PriorityQueue<Node, (double, int)>();
        
        var leftNode = GetLeftNode();
        
        if (leftNode is not null)
        {
            priorityQueue.Enqueue(leftNode, (-leftNode.Estimation, -leftNode.Level));
        }
        
        var rightNode = GetRightNode();
        priorityQueue.Enqueue(rightNode, (-rightNode.Estimation, -rightNode.Level));
        
        while (true)
        {
            var currentNode = priorityQueue.Dequeue();
        
            if (currentNode.Level == _itemsNumber)
            {
                var result = currentNode.ItemNumbers;
                result.Sort();
                return result;
            }
        
            leftNode = GetLeftNode(currentNode);
        
            if (leftNode is not null)
            {
                priorityQueue.Enqueue(leftNode, (-leftNode.Estimation, -leftNode.Level));
            }
        
            rightNode = GetRightNode(currentNode);
            priorityQueue.Enqueue(rightNode, (-rightNode.Estimation, -rightNode.Level));
        }
    }

    private Node? GetLeftNode()
    {
        if (_items[0].Weight > _maxWeight)
        {
            return null;
        }

        return new Node
        {
            Estimation = _items[0].Value + (_maxWeight - _items[0].Weight) * _items[1].UnitValue,
            Level = 1,
            ItemNumbers = new List<int> {_items[0].Number},
            Weight = _items[0].Weight,
            Value = _items[0].Value
        };
    }

    private Node? GetLeftNode(Node currentNode)
    {
        var level = currentNode.Level;
        var currentWeight = currentNode.Weight + _items[level].Weight;

        if (currentWeight > _maxWeight)
        {
            return null;
        }
        
        var leftNode = new Node
        {
            Estimation = currentNode.Value + _items[level].Value + (_maxWeight - currentWeight) * _items[level + 1].UnitValue,
            Level = level + 1,
            ItemNumbers = currentNode.ItemNumbers.ToList(),
            Weight = currentWeight,
            Value = currentNode.Value + _items[level].Value
        };
        
        leftNode.ItemNumbers.Add(_items[level].Number);
        return leftNode;
    }

    private Node GetRightNode() => new()
    {
        Estimation = _maxWeight * _items[1].UnitValue,
        Level = 1
    };

    private Node GetRightNode(Node currentNode) => new()
    {
        Estimation = currentNode.Value + (_maxWeight - currentNode.Weight) * _items[currentNode.Level + 1].UnitValue,
        Level = currentNode.Level + 1,
        ItemNumbers = currentNode.ItemNumbers.ToList(),
        Weight = currentNode.Weight,
        Value = currentNode.Value
    };
}