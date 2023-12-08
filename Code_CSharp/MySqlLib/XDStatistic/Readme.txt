XDStatistic 多维度交叉统计框架

用于从初级统计地表中，初级数据按照某种规则归类到几个单独的维度上，然后对着几个维度的全部交叉组合进行统计。
统计中的每一个Value都对应着一个完整的SQL 头部
不同的维度交叉，实际上就是在每个sql的 where 条件总加入不同的 维度条件的排列组合结果。


例如：

维度1	维度2	维度3	Value1=sum(aaa)	Value2=sum(bbb)	Value3=avg(ccc)	Value1=sum(aaa)/sum(bbb)
 1.1     2.1     3.1        xxx             xxx               xxx                xxx 
 1.2     2.1     3.1        xxx             xxx               xxx                xxx 
 1.3     2.1     3.1        xxx             xxx               xxx                xxx 
 1.1     2.2     3.1        xxx             xxx               xxx                xxx 
 1.1     2.2     3.2        xxx             xxx               xxx                xxx 
 1.1     2.2     3.3        xxx             xxx               xxx                xxx 

